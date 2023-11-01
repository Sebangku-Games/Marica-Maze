using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Dictionary<string, int> levelNameToIndex = new Dictionary<string, int>();


    [SerializeField] private LevelData[] levels; // An array of LevelData.
    [SerializeField] private Road _cellPrefab;

    private bool hasGameFinished;
    private Road[,] roads;
    private List<Road> startRoads;
    [SerializeField] private int currentLevelIndex = 0; // Keep track of the current level.

    public TextMeshProUGUI textTimer;
    public float Waktu;
    private float s;
    public bool GameAktif = true;
    public Text levelText;
    public bool isTutorialShowing = false;

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public GameObject WinText;
    public GameObject LoseText;
    public GameObject Tangkap;
    public TMP_Text amountClickText;

    //panel tutor
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;
    public GameObject Panel4;

    public GameObject starend1;
    public GameObject starend2;
    public GameObject starend3;

    [SerializeField] private LevelData _level; // Store the current level data.

    [SerializeField] public int amountClicked = 0;
    private bool clicksConditionMet = false;
    private bool timeConditionMet = false;

    private Achievements achievements;


    private void Awake()
    {
        Instance = this;
        hasGameFinished = false;

    }

    private void Start()
    {
        // get the current level from game data
        currentLevelIndex = GameData.InstanceData.currentLevel;
        // Load the initial level.
        //FindObjectOfType<Anim>().SetPosition();
        LoadLevel(currentLevelIndex);
        Waktu = _level.waktu;

        amountClicked = 0;

        achievements = FindObjectOfType<Achievements>();

        UpdateAmountClickText();

        if(currentLevelIndex == 0)
        {
            
            ShowTutorial();
            
        }
    }

    

    private void LoadLevel(int levelIndex)
    {
        WinText.SetActive(false);

        int currLevel = currentLevelIndex + 1;

        if (levelIndex < 0 || levelIndex >= levels.Length)
        {
            Debug.LogError("Invalid level index.");
            return;
        }

        // Set the levelText to display the current level index.
        if (levelText != null)
        {
            levelText.text = "LEVEL " + currLevel.ToString();
        }

        _level = levels[levelIndex]; // Update the current level data.
        SpawnLevel(_level);
    }

    private void DestroyLevel(int levelIndex)
    {
        if (roads == null) return;

        for (int i = 0; i < levels[levelIndex].Row; i++)
        {
            for (int j = 0; j < levels[levelIndex].Column; j++)
            {
                Destroy(roads[i, j].gameObject);
            }
        }
    }

    private void SpawnLevel(LevelData levelData)
    {
        roads = new Road[levelData.Row, levelData.Column];
        startRoads = new List<Road>();

        float xOffset = levelData.Column / 2.0f;
        float yOffset = levelData.Row / 2.0f;

        for (int i = 0; i < levelData.Row; i++)
        {
            for (int j = 0; j < levelData.Column; j++)
            {
                // Hitung posisi sel pipa agar berada di tengah
                Vector2 spawnPos = new Vector2(j - xOffset + 0.5f, i - yOffset + 0.5f);
                Road tempRoad = Instantiate(_cellPrefab, spawnPos, Quaternion.identity);

                // Dapatkan data dari List<int> Data
                int data = levelData.Data[i * levelData.Column + j];

                // Dapatkan tipe sel dan rotasi
                int type = data % 10; // Digit satuan adalah tipe sel.
                int rotation = data / 10; // Digit puluhan adalah rotasi.

                // Inisialisasi sel dengan tipe sel dan rotasi
                tempRoad.Init(type, rotation);

                roads[i, j] = tempRoad;
                if (type == 1)
                {
                    startRoads.Add(tempRoad);
                }
            }
        }

        StartCoroutine(ShowHint());
    }



    private void Update()
    {
        if (hasGameFinished) return;

        // Logika timer yang sudah Anda berikan.
        if (GameAktif)
        {
            s += Time.deltaTime;
            if (s >= 1)
            {
                Waktu--;
                s = 0;
            }
        }

        // Tampilkan timer di layar.
        int Menit = Mathf.FloorToInt(Waktu / 60);
        int Detik = Mathf.FloorToInt(Waktu % 60);
        textTimer.text = Menit.ToString("00") + ":" + Detik.ToString("00");

        if (GameAktif && Waktu <= 0)
        {
            LoseText.SetActive(true);
            GameAktif = false;
        }

        if (GameAktif)
        {
            if (!clicksConditionMet && !IsClearedWithXAmountClicks())
            {
                Deactivate1Star();
                clicksConditionMet = true;
            }

            if (!timeConditionMet && !IsClearedWithXAmountTime())
            {
                Deactivate1Star();
                timeConditionMet = true;
            }
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float xOffset = levels[currentLevelIndex].Column / 2.0f;
        float yOffset = levels[currentLevelIndex].Row / 2.0f;

        // Menggeser posisi mouse sesuai dengan offset
        float adjustedMouseX = mousePos.x + xOffset;
        float adjustedMouseY = mousePos.y + yOffset;

        int row = Mathf.FloorToInt(adjustedMouseY);
        int col = Mathf.FloorToInt(adjustedMouseX);

        if (row < 0 || col < 0) return;
        if (row >= levels[currentLevelIndex].Row) return;
        if (col >= levels[currentLevelIndex].Column) return;

        if (Input.GetMouseButtonDown(0))
        {
            roads[row, col].UpdateInput();
            StartCoroutine(ShowHint());

        }
    }

    private void Deactivate1Star()
    {
        // Deactivate stars in reverse order, starting from Star3 down to Star1
        if (Star3.activeSelf)
        {
            Star3.SetActive(false);
        }
        else if (Star2.activeSelf)
        {
            Star2.SetActive(false);
        }
        else if (Star1.activeSelf)
        {
            Star1.SetActive(false);
        }
    }

    private IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(0.1f);
        CheckFill();
        CheckWin();
        CheckLose();
    }

    private void CheckFill()
    {
        for (int i = 0; i < levels[currentLevelIndex].Row; i++)
        {
            for (int j = 0; j < levels[currentLevelIndex].Column; j++)
            {
                Road tempRoad = roads[i, j];
                if (tempRoad.RoadType != 0)
                {
                    tempRoad.IsFilled = false;
                }
            }
        }

        Queue<Road> check = new Queue<Road>();
        HashSet<Road> finished = new HashSet<Road>();
        foreach (var road in startRoads)
        {
            check.Enqueue(road);
        }

        while (check.Count > 0)
        {
            Road road = check.Dequeue();
            finished.Add(road);
            List<Road> connected = road.ConnectedRoads();
            foreach (var connectedRoad in connected)
            {
                if (!finished.Contains(connectedRoad))
                {
                    check.Enqueue(connectedRoad);
                }
            }
        }

        foreach (var filled in finished)
        {
            filled.IsFilled = true;
        }

        for (int i = 0; i < levels[currentLevelIndex].Row; i++)
        {
            for (int j = 0; j < levels[currentLevelIndex].Column; j++)
            {
                Road tempRoad = roads[i, j];
                tempRoad.UpdateFilled();
            }
        }
    }


    private void CheckWin()
    {
        // Memeriksa apakah sel pipa tipe 2 telah terisi.
        foreach (var road in roads)
        {
            if (road.RoadType == 2 && !road.IsFilled)
            {
                return; // Sel pipa tipe 2 belum terisi, permainan masih berlanjut.
            }
        }

        // Jika semua sel pipa tipe 2 terisi, maka permainan selesai.
        FindObjectOfType<Anim>().PlayAnim();
        hasGameFinished = true;
        // check if game finished in <= 2 seconds
        if (Waktu >= levels[currentLevelIndex].waktu - 2)
        {
            achievements.UnlockAchievement("ClearLessThan2Seconds");
        }
        StartCoroutine(GameFinished());
    }


    private IEnumerator GameFinished()
    {
        if (achievements.IsAllLevelInMap1Unlocked()){
            achievements.UnlockAchievement("Hutan");
        } 

        if (achievements.IsAllLevelInMap2Unlocked()){
            achievements.UnlockAchievement("Gurun");
        }

        if (achievements.IsAllLevelInMap3Unlocked()){
            achievements.UnlockAchievement("Es");
        }

        if (achievements.IsAllLevelInMap4Unlocked()){
            achievements.UnlockAchievement("Air");
        }
        
        if (achievements.IsAllLevelUnlocked()){
            achievements.UnlockAchievement("SeluruhWilayah");
        }
        

        if (currentLevelIndex == 19)
        {
            if (achievements.IsMetHewanBuas()){
                achievements.UnlockAchievement("HewanBuas");
            }
        }

        yield return new WaitForSeconds(2f);
        WinText.SetActive(true);
        DetermineStars();
        Time.timeScale = 0;
    }
    private void CheckLose()
    {
        bool roadType9 = false;
        foreach (var road in roads)
        {
            if (road.RoadType == 9)
            {
                roadType9 = true;
                if (!road.IsFilled)
                {
                    return;
                }
            }
        }
        if (!roadType9)
        {
            return;
        }
        if (roadType9){
            int currentLevel = GameData.InstanceData.currentLevel + 1; // current level bernilai 11 - 20
            SetBoolPlayerPrefs("KetemuHewanBuasLevel" + currentLevel.ToString(), true);
        }
        Debug.Log("game kalah");
        GameAktif = false;
        hasGameFinished = true;
        StartCoroutine(GameLose());
        FindObjectOfType<Anim>().PlayAnimLose();
    }

    private void SetBoolPlayerPrefs(string key, bool value){
        // Set an int based on the value bool
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    


    private IEnumerator GameLose()
    {
        yield return new WaitForSeconds(1.5f);
        Tangkap.SetActive(true);
        Time.timeScale = 0;
    }

    private bool IsClearedWithXAmountTime(){
        int currentLevel = GameData.InstanceData.currentLevel;

        if (currentLevel >= 0 && currentLevel < levels.Length)
        {
            if (Waktu >= levels[currentLevel].timerToGetStar)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private bool IsClearedWithXAmountClicks(){
        int currentLevel = GameData.InstanceData.currentLevel;

        if (currentLevel >= 0 && currentLevel < levels.Length)
        {
            if (amountClicked <= levels[currentLevel].amountClickToGetStar)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private void DetermineStars()
    {
        int starsEarned = 0;

        Debug.Log("Amount of clicks: " + amountClicked);

        // Periksa waktu yang tersisa dan tentukan berapa bintang yang diperoleh.
        if (IsClearedWithXAmountClicks() && IsClearedWithXAmountTime())
        {
            starsEarned = 3;
        }
        else if (IsClearedWithXAmountClicks() || IsClearedWithXAmountTime())
        {
            starsEarned = 2;
        }
        else
        {
            starsEarned = 1;
        }

        // Aktifkan GameObject bintang sesuai dengan jumlah bintang yang diperoleh.
        if (starsEarned >= 1)
        {
            starend1.SetActive(true);
        }
        if (starsEarned >= 2)
        {
            starend2.SetActive(true);
        }
        if (starsEarned >= 3)
        {
            starend3.SetActive(true);
        }

        ScoreManager.Instance.SetStarsScorePerLevel(starsEarned);

        Debug.Log("stars earned (not playerprefs) " + starsEarned);

        Debug.Log("Total stars earned: " + ScoreManager.Instance.GetTotalScore());
    }


    // public bool CheckIfClearOnEffiecientRoad(){
        

    //     return false;
    // }

    public bool CheckIfSpecificRoadIsFilled()
    {
        switch(currentLevelIndex){
            case 15 :
                if (roads == null)
                {
                    return true; // Return false if roads array is not initialized.
                }

                foreach (var road in roads)
                {
                    // Check if the road's type is 2, 3, 5, or 6 and if it is filled.
                    if (road.RoadType == 6 && road.IsFilled)
                    {
                        return false; // At least one road of the specified types is filled.
                    }
                }

                return true; // No road of the specified types is filled.

            case 16 :
                if (roads == null)
                {
                    return true; // Return false if roads array is not initialized.
                }

                foreach (var road in roads)
                {
                    // Check if the road's type is 2, 3, 5, or 6 and if it is filled.
                    if (road.RoadType == 8 && road.IsFilled)
                    {
                        return false; // At least one road of the specified types is filled.
                    }
                }

                return true; // No road of the specified types is filled.
            case 17:
                if (roads == null)
                {
                    return true; // Return false if roads array is not initialized.
                }

                foreach (var road in roads)
                {
                    // Check if the road's type is 2, 3, 5, or 6 and if it is filled.
                    if (road.RoadType == 7 && road.IsFilled)
                    {
                        return false; // At least one road of the specified types is filled.
                    }
                }

                return true; // No road of the specified types is filled.
            case 18:
                if (roads == null)
                {
                    return true; // Return false if roads array is not initialized.
                }

                foreach (var road in roads)
                {
                    // Check if the road's type is 2, 3, 5, or 6 and if it is filled.
                    if (road.RoadType == 8 && road.IsFilled)
                    {
                        return false; // At least one road of the specified types is filled.
                    }
                }

                return true; // No road of the specified types is filled.
            case 19:
                if (roads == null)
                {
                    return true; // Return false if roads array is not initialized.
                }

                foreach (var road in roads)
                {
                    // Check if the road's type is 2, 3, 5, or 6 and if it is filled.
                    if (road.RoadType == 7 && road.IsFilled)
                    {
                        return false; // At least one road of the specified types is filled.
                    }
                }

                return true; // No road of the specified types is filled.


        }
        return false;
        
    }

    public void UpdateAmountClickText(){
        int remainingClicks = levels[currentLevelIndex].amountClickToGetStar - amountClicked;
        amountClickText.text = remainingClicks < 0 ? "0" : remainingClicks.ToString();
    }


    // public void NextLevel(){
    //     if (currentLevelIndex >= levels.Length - 1)
    //     {
    //         Debug.Log("No more levels!");
    //         SceneManager.LoadScene("ChooseLevel");
    //         return;
    //     }
        
    //     // Reload scene but load the next level
    //     GameData.InstanceData.currentLevel++;
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //     Time.timeScale = 1;
    // }

    public void ShowTutorial(){
        isTutorialShowing = true;
        Time.timeScale = 0;
        Panel1.SetActive(true);
    }

    public void HideTutorial(){
        isTutorialShowing = false;
        Time.timeScale = 1;
        Panel1.SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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

    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public GameObject WinText;
    public GameObject LoseText;

    public GameObject starend1;
    public GameObject starend2;
    public GameObject starend3;

    [SerializeField] private LevelData _level; // Store the current level data.

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
        LoadLevel(currentLevelIndex);
        Waktu = _level.waktu;
    }

    private void LoadLevel(int levelIndex)
    {
        WinText.SetActive(false);

        if (levelIndex < 0 || levelIndex >= levels.Length)
        {
            Debug.LogError("Invalid level index.");
            return;
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

        if (GameAktif && Waktu <= 25)
        {
            Star3.SetActive(false);
        }
        if (GameAktif && Waktu <= 15)
        {
            Star2.SetActive(false);
        }
        if (GameAktif && Waktu <= 5)
        {
            Star1.SetActive(false);
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

    private IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(0.1f);
        CheckFill();
        CheckWin();
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
        hasGameFinished = true;
        StartCoroutine(GameFinished());
    }


    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(1f);
        WinText.SetActive(true);
        DetermineStars();
        Time.timeScale = 0;
    }

    private void DetermineStars()
    {
        int starsEarned = 0;

        // Periksa waktu yang tersisa dan tentukan berapa bintang yang diperoleh.
        if (Waktu >= 25)
        {
            starsEarned = 3;
        }
        else if (Waktu >= 15)
        {
            starsEarned = 2;
        }
        else if (Waktu >= 5)
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

        Debug.Log("Total stars earned: " + ScoreManager.Instance.GetTotalScore());
    }

    public void NextLevel(){
        if (currentLevelIndex >= levels.Length - 1)
        {
            Debug.Log("No more levels!");
            return;
        }
        
        // Reload scene but load the next level
        GameData.InstanceData.currentLevel++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }


}

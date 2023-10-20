using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Map 1 (Level 1 - 5)")]
    public GameObject[] starLevelMap1;

    
    [Header("Map 2 (Level 6 - 10)")]
    public GameObject[] starLevelMap2;
    
    [Header("Map 3 (Level 11 - 15)")]
    public GameObject[] starLevelMap3;
    
    [Header("Map 4 (Level 16 - 20)")]
    public GameObject[] starLevelMap4;

    [Header("Locked Sprite")]
    public GameObject[] lockedSprite;

    private void Start()
    {
        SetAllLevelToLocked();

        DeactivateStars(starLevelMap1);
        DeactivateStars(starLevelMap2);
        DeactivateStars(starLevelMap3);
        DeactivateStars(starLevelMap4);

        CheckStarsFromPlayerPrefs();
    }

    private void DeactivateStars(GameObject[] starArray)
    {
        foreach (GameObject star in starArray)
        {
            foreach (Transform child in star.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void SetAllLevelToLocked()
    {
        // Set all level to locked
        for (int i = 1; i < lockedSprite.Length; i++)
        {
            // set sprite on first child to true
            lockedSprite[i].transform.GetChild(0).gameObject.SetActive(true);

            // set button on this object to not interactable
            lockedSprite[i].GetComponent<UnityEngine.UI.Button>().interactable = false;
        }
    }

    public void UnlockLevel(int level)
    {
        int levelBefore = level - 1;
        // check if levelBefore already have 3 stars
        if (PlayerPrefs.HasKey(levelBefore.ToString()))
        {
            if (PlayerPrefs.GetInt(levelBefore.ToString()) == 3)
            {
                // set sprite on first child to false
                lockedSprite[levelBefore].transform.GetChild(0).gameObject.SetActive(false);

                // set button on this object to interactable
                lockedSprite[levelBefore].GetComponent<UnityEngine.UI.Button>().interactable = true;
            }
        }
    }



    public void CheckStarsFromPlayerPrefs(){
        for (int i = 0; i < 20; i++)
        {
            if (PlayerPrefs.HasKey((i+1).ToString()))
            {
                if (PlayerPrefs.GetInt((i+1).ToString()) == 1)
                {
                    starLevelMap1[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else if (PlayerPrefs.GetInt((i+1).ToString()) == 2)
                {
                    starLevelMap1[i].transform.GetChild(0).gameObject.SetActive(true);
                    starLevelMap1[i].transform.GetChild(1).gameObject.SetActive(true);
                }
                else if (PlayerPrefs.GetInt((i+1).ToString()) == 3)
                {
                    starLevelMap1[i].transform.GetChild(0).gameObject.SetActive(true);
                    starLevelMap1[i].transform.GetChild(1).gameObject.SetActive(true);
                    starLevelMap1[i].transform.GetChild(2).gameObject.SetActive(true);

                    UnlockLevel(i+2);
                }
            }
        }
    }
    
    

    public void LoadLevel(int level){
        if(level < 6 && level > 0){
            GameData.InstanceData.currentLevel = level-1;
            SceneManager.LoadScene("Level1-5");
        }
        else if (level < 11 && level > 5){
            GameData.InstanceData.currentLevel = level-1;
            SceneManager.LoadScene("Level6-10");
        }
        else if (level < 16 && level > 10){
            GameData.InstanceData.currentLevel = level-1;
            SceneManager.LoadScene("Level11-15");
        }
        else if (level < 21 && level > 15){
            GameData.InstanceData.currentLevel = level-1;
            SceneManager.LoadScene("Level16-20");
        }
    }
}

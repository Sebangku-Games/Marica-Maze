using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    public void SetStarsScorePerLevel(int stars)
    {
        // Get current level based on scene
        string currentLevel = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // Extract the level number from the scene name (assuming the scene names are like "LevelX")
        int levelNumber;
        if (int.TryParse(currentLevel.Replace("Level", ""), out levelNumber) && levelNumber >= 1 && levelNumber <= 20)
        {
            // Check if the levelkey exist in playerprefs
            if (PlayerPrefs.HasKey(currentLevel))
            {
                // If exist, check if the current score is higher than the previous score
                if (PlayerPrefs.GetInt(currentLevel) < stars)
                {
                    // If higher, save the new score
                    PlayerPrefs.SetInt(currentLevel, stars);
                    AddScoreToTotalScore(stars - PlayerPrefs.GetInt(currentLevel));
                }
            }
            else
            {
                // If not exist, save the new score
                PlayerPrefs.SetInt(currentLevel, stars);
                AddScoreToTotalScore(stars);
            }
        }
        Debug.Log("stars earned on this level: " + stars);
    }


    private void AddScoreToTotalScore(int score){
        // Add score to total score
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + score);
    }

    public int GetTotalScore(){
        return PlayerPrefs.GetInt("TotalScore");
    }
}

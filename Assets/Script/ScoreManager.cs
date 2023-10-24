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
        int currentLevel = GameData.InstanceData.currentLevel + 1;
        
        if (currentLevel >= 1 && currentLevel <= 20)
        {
            // Check if the levelkey exist in playerprefs
            if (PlayerPrefs.HasKey(currentLevel.ToString()))
            {
                // If exist, check if the current score is higher than the previous score
                if (PlayerPrefs.GetInt(currentLevel.ToString()) < stars)
                {
                    // If higher, save the new score
                    PlayerPrefs.SetInt(currentLevel.ToString(), stars);
                    AddScoreToTotalScore(stars - PlayerPrefs.GetInt(currentLevel.ToString()));
                }
            }
            else
            {
                // If not exist, save the new score
                PlayerPrefs.SetInt(currentLevel.ToString(), stars);
                AddScoreToTotalScore(stars);
            }
        }
        Debug.Log("Stars earned on level" + currentLevel + ": " + PlayerPrefs.GetInt(currentLevel.ToString()));
    }


    private void AddScoreToTotalScore(int score){
        // Add score to total score
        PlayerPrefs.SetInt("TotalScore", PlayerPrefs.GetInt("TotalScore") + score);
    }

    public int GetTotalScore(){
        return PlayerPrefs.GetInt("TotalScore");
    }

    public void DeletePlayerPrefs(){ // For debugging to reset playerprefs data
        PlayerPrefs.DeleteAll();
    }
}

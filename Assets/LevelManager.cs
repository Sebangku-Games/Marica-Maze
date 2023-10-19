using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    

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

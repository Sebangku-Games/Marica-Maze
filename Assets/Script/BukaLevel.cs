using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BukaLevel: MonoBehaviour
{
    public int Levelyangterbuka = 2;  // Set the level to unlock after winning

    private void Start()
    {
        // Assuming you have a PlayerPrefs key to store the highest unlocked level
        int highestUnlockedLevel = PlayerPrefs.GetInt("highestUnlockedLevel", 1);

        // Check if the current level is the one to unlock
        if (Levelyangterbuka > highestUnlockedLevel)
        {
            PlayerPrefs.SetInt("highestUnlockedLevel", Levelyangterbuka);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Achievements : MonoBehaviour
{
    const string Ach_ClearLessThan2Seconds = "CgkIi8Dh0-sKEAIQAw";

    const string Ach_IdTestLOL = "CgkIi8Dh0-sKEAIQBA";

    //[SerializeField] private GameObject loginPanel;

    public void ShowAchievementUI()
    {
        // check if player is authenticated
        if (Social.localUser.authenticated) // player has login
        {
            // show achievement
            Social.ShowAchievementsUI();
        } else { 
            // player hasnt login, try authenticate
            Social.localUser.Authenticate(success =>
            {
                if (success)
                {
                    Social.ShowAchievementsUI();
                }
                else
                { 
                    //loginPanel.SetActive(true);
                }
            });
        }
    }


    // unlock achievement ZETAAA UWEEEEEEEEEEEEEEEEEEE
    public void UnlockAchievementLOL(){
        ReportProgressAchievement(Ach_IdTestLOL, 100f);
    }

    // unlock achievement for each scene
    public void UnlockAchievement()
    {
        ReportProgressAchievement(Ach_ClearLessThan2Seconds, 100f);
    }


    public void ReportProgressAchievement(string achievementId, float progress){
        Social.ReportProgress(achievementId, progress, success => {
            if (success)
            {
                Debug.Log("Achievement Unlocked");
            }
            else
            {
                Debug.Log("Achievement Failed");
            }
        });
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadsistem : MonoBehaviour
{
    public GameObject Pause;
    private bool isGamePaused = false;


    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;

    }
    public void pausegame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        Pause.SetActive(true);

        // Tambahan: Jika ingin menghentikan rotasi pada objek Road
        //Road[] roads = FindObjectsOfType<Road>();
       // foreach (Road road in roads)
        {
            //road.PauseRotation();
        }
    }

    public void resumegame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
        Pause.SetActive(false);

        // Tambahan: Jika ingin melanjutkan rotasi pada objek Road
       // Road[] roads = FindObjectsOfType<Road>();
        //foreach (Road road in roads)
        {
          //  road.ResumeRotation();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public AudioSource music;
    public GameObject Pause;



    public void keluar()
    {
        Application.Quit();
    }
    public void volume(float vol)
    {
        music.volume = vol;
    }

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
        Time.timeScale = 0;
        Pause.SetActive(true);
    }
    public void resumegame()
    {
        Time.timeScale = 1;
        Pause.SetActive(false);

    }
}
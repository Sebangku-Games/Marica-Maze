using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenu : MonoBehaviour
{
    public AudioSource music;
    public Toggle soundToggle;

    private bool isSoundOn = true;
    public void Start()
    {
        soundToggle.isOn = isSoundOn;
        SetSound(isSoundOn);
    }
    public void keluar()
    {
        Application.Quit();
    }
    public void ToggleSuara()
    {
        isSoundOn = !isSoundOn; // Toggle status suara
        SetSound(isSoundOn); // Mengatur suara berdasarkan status baru
    }

    private void SetSound(bool enableSound)
    {
        if (music != null)
        {
            music.volume = enableSound ? 1.0f : 0.0f;
        }
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

}
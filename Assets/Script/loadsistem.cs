using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadsistem : MonoBehaviour
{
    public GameObject Pause;
 
   
    
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

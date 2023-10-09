using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagemen : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    int NomorLevelterbuka;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("levelyangterbuka"))
        {
            PlayerPrefs.SetInt("levelyangterbuka", 1);
        }
        NomorLevelterbuka = PlayerPrefs.GetInt("levelyangterbuka");
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
    }
    private void Update()
    {
        NomorLevelterbuka = PlayerPrefs.GetInt("levelyangterbuka");
        for (int i = 0; i < NomorLevelterbuka; i++)
        {
            buttons[i].interactable = true;

        }
    }
}

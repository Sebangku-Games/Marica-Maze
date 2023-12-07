using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManagemen : MonoBehaviour
{
    private Animator animator;

    [SerializeField] Button[] buttons;
    int NomorLevelterbuka;
    private void Start()
    {
        animator = GetComponent<Animator>();

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

     public void buka()
    {
        animator.SetTrigger("Buka");
    }
}

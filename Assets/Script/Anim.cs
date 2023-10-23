using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent < Animator>();
        SetPosition(); // Panggil SetPosition saat objek pertama kali dimulai.
    }

    public void SetPosition()
    {
        switch (GameData.InstanceData.currentLevel)
        {
            case 0:
                // set this gameobject position to 0,0,0
                transform.parent.position = new Vector3(1, -1, 0);
                Debug.Log("set0");
                break;
            case 1:
                Debug.Log("Set1");
                break;
            case 2:
                Debug.Log("Set2");
                break;
            case 3:
                Debug.Log("Set3");
                break;
            default:
                break;
        }
    }

    public void PlayAnim()
    {
        switch (GameData.InstanceData.currentLevel)
        {
            case 0:
                animator.SetTrigger("tgrFinish1");
                Debug.Log("f1");
                break;
            case 1:
                animator.SetTrigger("tgrFinish2");
                break;
        }
    }

    private void Update()
    {
        if (animator != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayAnim();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent < Animator>();
        SetPosition(); 
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
                transform.parent.position = new Vector3(-1, 1, 0);
                Debug.Log("Set1");
                break;
            case 2:
                transform.parent.position = new Vector3(-1, -1, 0);
                break;
            case 3:
                transform.parent.position = new Vector3(-1, -1, 0);
                break;
            case 4:
                transform.parent.position = new Vector3(1, -1, 0);
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
            case 2:
                animator.SetTrigger("tgrFinish3");
                break;
            case 3:
                animator.SetTrigger("tgrFinish4");
                break;
            case 4:
                animator.SetTrigger("tgrFinish5");
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

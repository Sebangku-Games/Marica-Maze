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
            case 5:
                transform.parent.position = new Vector3(-1, -1, 0);
                break;
            case 6:
                transform.parent.position = new Vector3(1f, -0.5f, 0f);
                break;
            case 7:
                transform.parent.position = new Vector3(-1f, -1.5f, 0f);
                break;
            case 8:
                transform.parent.position = new Vector3(1f, -1.5f, 0f);
                break;
            case 9:
                transform.parent.position = new Vector3(0f, -1.5f, 0f);
                break;
            case 10:
                transform.parent.position = new Vector3(-1.5f, -1.5f, 0f);
                break;
            case 11:
                transform.parent.position = new Vector3(-1.5f, 1.5f, 0f);
                break;
            case 12:
                transform.parent.position = new Vector3(1.5f, -1.5f, 0f);
                break;
            case 13:
                transform.parent.position = new Vector3(1.5f, -1.5f, 0f);
                break;
            case 14:
                transform.parent.position = new Vector3(1.5f, 2f, 0f);
                break;
            case 15:
                transform.parent.position = new Vector3(-1.5f, -1f, 0f);
                break;
            case 16:
                transform.parent.position = new Vector3(-1.5f, -2.5f, 0f);
                break;
            case 17:
                transform.parent.position = new Vector3(-1.5f, -2.5f, 0f);
                break;
            case 18:
                transform.parent.position = new Vector3(-1.5f, -2.5f, 0f);
                break;
            case 19:
                transform.parent.position = new Vector3(-1.5f, -2.5f, 0f);
                break;
        }
    }

    public void PlayAnim()
    {
        switch (GameData.InstanceData.currentLevel)
        {
            case 0:
                animator.SetTrigger("tgrFinish1");
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
            case 5:
                animator.SetTrigger("tgrFinish6");
                break;
            case 6:
                animator.SetTrigger("tgrFinish7");
                break;
            case 7:
                animator.SetTrigger("tgrFinish8");
                break;
            case 8:
                animator.SetTrigger("tgrFinish9");
                break;
            case 9:
                animator.SetTrigger("tgrFinish10");
                break;
            case 10:
                animator.SetTrigger("tgrFinish11");
                break;
            case 11:
                animator.SetTrigger("tgrFinish12");
                break;
            case 12:
                animator.SetTrigger("tgrFinish13");
                break;
            case 13:
                animator.SetTrigger("tgrFinish14");
                break;
            case 14:
                animator.SetTrigger("tgrFinish15");
                break;
            case 15:
                if (GameManager.Instance.CheckIfSpecificRoadIsFilled())
                {
                    animator.SetTrigger("tgrFinish16(2)");
                    Debug.Log("Play efficient anim");
                }
                else
                {
                    animator.SetTrigger("tgrFinish16");
                    Debug.Log("Play NOT efficient anim");
                }
                
                break;
            case 16:
                if (GameManager.Instance.CheckIfSpecificRoadIsFilled())
                {
                    animator.SetTrigger("tgrFinish17");
                    Debug.Log("Play efficient anim");
                }
                else
                {
                    animator.SetTrigger("tgrFinish17(2)");
                    Debug.Log("Play NOT efficient anim");
                }
                
                break;
                case 17:
                if (GameManager.Instance.CheckIfSpecificRoadIsFilled())
                {
                    animator.SetTrigger("tgrFinish18");
                    Debug.Log("Play efficient anim");
                }
                else
                {
                    animator.SetTrigger("tgrFinish18(2)");
                    Debug.Log("Play NOT efficient anim");
                }

                break;
            case 18:
                if (GameManager.Instance.CheckIfSpecificRoadIsFilled())
                {
                    animator.SetTrigger("tgrFinish19(2)");
                    Debug.Log("Play efficient anim");
                }
                else
                {
                    animator.SetTrigger("tgrFinish19");
                    Debug.Log("Play NOT efficient anim");
                }

                break;
            case 19:
                if (GameManager.Instance.CheckIfSpecificRoadIsFilled())
                {
                    animator.SetTrigger("tgrFinish20");
                    Debug.Log("Play efficient anim");
                }
                else
                {
                    animator.SetTrigger("tgrFinish20(2)");
                    Debug.Log("Play NOT efficient anim");
                }

                break;
        }
    }

    public void PlayAnimLose()
    {
        switch (GameData.InstanceData.currentLevel)
        {
            case 10:
                animator.SetTrigger("tgrFinish11(lose)");
                break;
            case 11:
                animator.SetTrigger("tgrFinish12(lose)");
                break;
            case 12:
                animator.SetTrigger("tgrFinish13(lose)");
                break;
            case 13:
                animator.SetTrigger("tgrFinish14(lose)");
                break;
            case 14:
                animator.SetTrigger("tgrFinish15(lose)");
                break;
            case 15:
                animator.SetTrigger("tgrFinish16(lose)");
                break;
            case 16:
                animator.SetTrigger("tgrFinish17(lose)");
                break;
            case 17:
                animator.SetTrigger("tgrFinish18(lose)");
                break;
            case 18:
                animator.SetTrigger("tgrFinish19(lose)");
                break;
            case 19:
                animator.SetTrigger("tgrFinish20(lose)");
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

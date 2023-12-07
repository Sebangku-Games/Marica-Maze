using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwanAnim : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void buka()
    {
        animator.SetTrigger("Buka");
    }

}

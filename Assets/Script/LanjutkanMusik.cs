using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanjutkanMusik : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musikObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musikObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

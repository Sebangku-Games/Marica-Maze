using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    bool selesai = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    public void cek()
    {
        for (int i = 0; i < 4; i++)
        {
            if (transform.GetChild(i).GetComponent<drag>().on_tempel)
            {
                selesai = true;
                Debug.Log("Selesai");
            }
            else
            {
                selesai = false;
                i = 4;
                Debug.Log("Salah");
            }
        }
    }

    void Update()
    {
        if (!selesai)
        {
            cek();
        }
    }
}

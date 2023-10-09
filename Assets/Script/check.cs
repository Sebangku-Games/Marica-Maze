using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check : MonoBehaviour
{
    [SerializeField] public GameObject menang;
    bool selesai = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!selesai)
        {
            cek();
        }

        // Memeriksa apakah semua empat anak objek telah ditempelkan
        if (transform.childCount == 4)
        {
            bool semuaDitempelkan = true;
            for (int i = 0; i < 4; i++)
            {
                drag childDrag = transform.GetChild(i).GetComponent<drag>();
                if (childDrag != null && !childDrag.on_tempel)
                {
                    semuaDitempelkan = false;
                    break; // Keluar dari loop segera jika ada satu yang belum ditempelkan
                }
            }

            if (semuaDitempelkan)
            {
                menang.gameObject.SetActive(true);
                selesai = true;
            }
        }
    }

    public void cek()
    {
        selesai = true; // Mengasumsikan true sampai dibuktikan sebaliknya
        for (int i = 0; i < 4; i++)
        {
            drag childDrag = transform.GetChild(i).GetComponent<drag>();
            if (childDrag != null && childDrag.on_tempel)
            {
                Debug.Log("Selesai");
                return; // Exit the loop early since the condition is met
            }
            else
            {
                selesai = false; // Mark as false, but keep checking other children
            }
        }
        Debug.Log("Salah");
    }
}

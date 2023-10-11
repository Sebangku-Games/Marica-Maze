using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    public GameObject detector;
    Vector3 posAwal;
    public bool on_pos = false, on_tempel = false;
    // Start is called before the first frame update
    void Start()
    {
        posAwal = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnMouseDrag()
    {
        Vector3 pos_mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.y));
        transform.position = new Vector3(pos_mouse.x, pos_mouse.y, -1f);
    }

    void OnMouseUp()
    {
        if (on_pos)
        {
            transform.position = detector.transform.position;
            on_tempel = true;
        }
        else
        {
            transform.position = posAwal;
            on_tempel = false;
        }
    }

    void OnTriggerStay2D(Collider2D objek)
    {
        if(objek.gameObject == detector)
        {
            on_pos = true;
        }
    }

    void OnTriggerExit2D(Collider2D objek)
    {
        if(objek.gameObject == detector)
        {
            on_pos = false;
        }
    }
    


}

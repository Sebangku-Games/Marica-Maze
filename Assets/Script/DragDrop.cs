
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public List<GameObject> correctForms; // Menggunakan List<GameObject> untuk menyimpan lebih dari satu objek yang benar

    public LevelSpawner levelSpawner;
    private bool isDragging;
    private Vector3 startPosition;
    private Vector3 resetPosition;

    void Start()
    {
        resetPosition = transform.localPosition;

        levelSpawner = GameObject.Find("Level Spawner").GetComponent<LevelSpawner>();

        // Menginisialisasi correctForms
        correctForms = new List<GameObject>
        {
            levelSpawner.transform.GetChild(0).gameObject
        };
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(worldMousePos.x - startPosition.x, worldMousePos.y - startPosition.y, transform.position.z);
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
            startPosition = worldMousePos - transform.position;
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        bool isCorrect = false;

        foreach (GameObject form in correctForms)
        {
            float distanceToCorrectForm = Vector3.Distance(transform.position, form.transform.position);
            if (distanceToCorrectForm <= 0.5f)
            {
                transform.position = form.transform.position;
                isCorrect = true;
                break; // Keluar dari loop jika sudah menemukan objek yang cocok
            }
        }

        if (!isCorrect)
        {
            transform.localPosition = resetPosition;
        }
    }
}

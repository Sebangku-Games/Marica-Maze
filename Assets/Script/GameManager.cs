using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject RoadHolder;
    public GameObject[] Road;
    public TextMeshProUGUI textTimer;
    public float Waktu = 100;
    public bool GameAktif = true;
    float s;

    [SerializeField]
    int totalRoad = 0;
    [SerializeField]
    int correctedRoad = 0;

    public GameObject WinText;
    public GameObject LoseText;
    void Start()
    {
        WinText.SetActive(false);
        totalRoad = RoadHolder.transform.childCount;

        Road= new GameObject[totalRoad];
        for (int i = 0; i < Road.Length; i++)
        {
            Road[i]= RoadHolder.transform.GetChild(i).gameObject;
        }
        
    }
    private void Update()
    {
        if (GameAktif)
        {
            s += Time.deltaTime;
            if (s >= 1)
            {
                Waktu--;
                s = 0;
            }
        }
        if (GameAktif && Waktu<= 0)
        {
            Debug.Log("Lose");
            LoseText.SetActive(true);
            GameAktif = false;
            StopAllRotations();
        }
        SetText();
    }
    void SetText()
    {
        int Menit = Mathf.FloorToInt(Waktu / 60);
        int Detik = Mathf.FloorToInt(Waktu % 60);
        textTimer.text = Menit.ToString("00") + ":" + Detik.ToString("00");
    }
    public void correctMove()
    {
        correctedRoad += 1;

        Debug.Log("correct Move");

        if (correctedRoad == totalRoad)
        {
            Debug.Log("You win!");
            WinText.SetActive(true);
            Time.timeScale = 0; 
            StopAllRotations(); 
        }
    }
    public void wrongMove()
    {
        correctedRoad -= 1;
    }
    private void StopAllRotations()
    {
        foreach (GameObject road in Road)
        {
            Road roadScript = road.GetComponent<Road>();
            if (roadScript != null)
            {
                roadScript.PauseRotation();
            }
        }
    }
}

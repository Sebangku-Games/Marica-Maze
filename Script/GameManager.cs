using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject RoadHolder;
    public GameObject[] Road;

    [SerializeField]
    int totalRoad = 0;
    [SerializeField]
    int correctedRoad = 0;

    public GameObject WinText;
    // Start is called before the first frame update
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
    public void correctMove()
    {
        correctedRoad += 1;

        Debug.Log("correct Move");

        if (correctedRoad == totalRoad)
        {
            Debug.Log("You win!");
            WinText.SetActive(true);
        }
    }
    public void wrongMove()
    {
        correctedRoad -= 1;
    }
}

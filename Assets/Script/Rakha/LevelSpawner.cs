using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cellObject;
    [SerializeField] private LevelSO levelSO;

    [SerializeField] private float offsetX = -1;
    [SerializeField] private float offsetY = 1;

    private void Start()
    {
        SetOffsetCells();
        SpawnCell(levelSO);
    }

    // spawn cellObject based on row/column of LevelSO
    public void SpawnCell(LevelSO levelSO)
    {
        for (int i = 0; i < levelSO.row; i++)
        {
            for (int j = 0; j < levelSO.column; j++)
            {
                GameObject cell = Instantiate(cellObject, new Vector3(j + offsetX, i + offsetY, 0), Quaternion.identity);
                cell.transform.parent = this.transform;
            }
        }
    }

    private void SetOffsetCells(){
        switch (levelSO.row){
            case 3 :
                offsetX = -1;
                offsetY = 1;
                break;

            case 4 : 
                offsetX = -1.5f;
                offsetY = 0.5f;
                break;

            case 5 :
                offsetX = -2;
                offsetY = 0;
                break;
        }
    }
}

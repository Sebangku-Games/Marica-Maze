using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class LevelSO : ScriptableObject
{
    public int level;
    public int row;
    public int column;
    public GameObject[] roadPrefabs;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData InstanceData;

    public int currentLevel = 0;

    private void Awake()
    {
        if (InstanceData == null)
        {
            InstanceData = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}

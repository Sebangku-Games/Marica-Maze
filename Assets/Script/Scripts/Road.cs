using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    [HideInInspector] public bool IsFilled;
    [HideInInspector] public int RoadType;

    [SerializeField] private Transform[] _roadPrefabs;

    private Transform currentRoad;
    private int rotation;

    private bool isRotationPaused = false;
    private SpriteRenderer emptySprite;
    private SpriteRenderer filledSprite;
    private List<Transform> connectBoxes;

    private const int minRotation = 0;
    private const int maxRotation = 3;
    private const int rotationMultiplier = 90;

    public void PauseRotation()
    {
        isRotationPaused = true;
    }

    public void ResumeRotation()
    {
        isRotationPaused = false;
    }
    public void Init(int road)
    {
        RoadType = road % 10;
        currentRoad = Instantiate(_roadPrefabs[RoadType], transform);
        currentRoad.transform.localPosition = Vector3.zero;
        if (RoadType == 1 || RoadType == 2)
        {
            rotation = road / 10;
        }
        else
        {
            rotation = Random.Range(minRotation, maxRotation + 1);
        }
        currentRoad.transform.eulerAngles = new Vector3(0, 0, rotation * rotationMultiplier);

        if (RoadType == 0 || RoadType == 1)
        {
            IsFilled = true;
        }

        if (RoadType == 0)
        {
            return;
        }

        emptySprite = currentRoad.GetChild(0).GetComponent<SpriteRenderer>();
        emptySprite.gameObject.SetActive(!IsFilled);
        filledSprite = currentRoad.GetChild(1).GetComponent<SpriteRenderer>();
        filledSprite.gameObject.SetActive(IsFilled);

        connectBoxes = new List<Transform>();
        for (int i = 2; i < currentRoad.childCount; i++)
        {
            connectBoxes.Add(currentRoad.GetChild(i));
        }
    }

    public void UpdateInput()
    {
        if (RoadType == 0 || RoadType == 1 || RoadType == 2)
        {
            return;
        }

        rotation = (rotation + 1) % (maxRotation + 1);
        currentRoad.transform.eulerAngles = new Vector3(0, 0, rotation * rotationMultiplier);
    }

    public void UpdateFilled()
    {
        if (RoadType == 0) return;
        emptySprite.gameObject.SetActive(!IsFilled);
        filledSprite.gameObject.SetActive(IsFilled);
    }

    public List<Road> ConnectedRoads()
    {
        List<Road> result = new List<Road>();

        foreach (var box in connectBoxes)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(box.transform.position, Vector2.zero, 0.1f);
            for (int i = 0; i < hit.Length; i++)
            {
                result.Add(hit[i].collider.transform.parent.parent.GetComponent<Road>());
            }
        }

        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private const int maxRotation = 3;
    [SerializeField] private const int rotationMultiplier = 90;

    [SerializeField] private int rotation = 0;

    [SerializeField] private bool isStartLine;
    [SerializeField] private bool isFinishLine;
    [SerializeField] private bool isFilled;

    public bool isStartPointConnectedToAnyEndPoint;
    public bool isEndPointConnectedToAnyStartPoint;

    private Road connectedStartPoint;
    private Road connectedEndPoint;

    [SerializeField] private BoxCollider2D startPoint;
    [SerializeField] private BoxCollider2D endPoint;

    private SpriteRenderer emptySprite;
    private SpriteRenderer filledSprite;

    private void Awake()
    {
        startPoint = transform.GetChild(0).GetComponent<BoxCollider2D>();
        endPoint = transform.GetChild(1).GetComponent<BoxCollider2D>();

        emptySprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
        filledSprite = transform.GetChild(3).GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        UpdateFilled();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.transform == transform)
            {
                if (!isStartLine && !isFinishLine)
                {
                    Rotate();
                }
            }
        }
    }

    private void Rotate()
    {
        rotation = (rotation + 1) % (maxRotation + 1);
        transform.eulerAngles = new Vector3(0, 0, rotation * rotationMultiplier);
    }

    public void UpdateFilled()
    {
        // Check if this Road is connected to both a StartPoint and EndPoint
        bool isConnected = IsAllSideConnected();

        // Condition 1: StartLine and FinishLine always have isFilled set to true
        if (isStartLine || isFinishLine)
        {
            isFilled = true;
        }
        // Condition 2: Check if this Road is connected
        else if (isConnected)
        {
            isFilled = true;
        }
        else
        {
            isFilled = false;
        }

        emptySprite.gameObject.SetActive(!isFilled);
        filledSprite.gameObject.SetActive(isFilled);
    }

    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("OnTriggerEnter2D is called from " + this.gameObject.tag + " to " + other.gameObject.tag);
    //     Road road = other.GetComponentInParent<Road>();
    //     if (road != null)
    //     {
    //         if (other.CompareTag("EndPoint"))
    //         {
    //             // Set the connected EndPoint reference
    //             connectedEndPoint = road;
    //             UpdateFilled();
    //         }
    //         else if (other.CompareTag("StartPoint"))
    //         {
    //             // Set the connected StartPoint reference
    //             connectedStartPoint = road;
    //             UpdateFilled();
    //         }
    //     }
    // }

    

    private bool IsAllSideConnected(){
        return isStartPointConnectedToAnyEndPoint && isEndPointConnectedToAnyStartPoint;
    }
}
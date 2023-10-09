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
        // Condition 1: StartLine always has isFilled set to true
        if (isStartLine || isFinishLine)
        {
            isFilled = true;
        }
        else
        {
            // Condition 2: Check if this is connected to a StartLine endPoint
            if (startPointConnectedToStartLine() || endPointConnectedToFinishLine())
            {
                isFilled = true;
            }
        }

        emptySprite.gameObject.SetActive(!isFilled);
        filledSprite.gameObject.SetActive(isFilled);
    }

    private bool startPointConnectedToStartLine()
    {
        // Check if this startPoint is connected to any StartLine endPoint
        Collider2D[] colliders = Physics2D.OverlapBoxAll(startPoint.bounds.center, startPoint.bounds.size, 0f);

        foreach (Collider2D collider in colliders)
        {
            Road road = collider.GetComponent<Road>();
            if (road != null && road.isStartLine && road.endPointConnectedTo(this.startPoint))
            {
                return true;
            }
        }

        return false;
    }

    private bool endPointConnectedToFinishLine()
    {
        // Check if this endPoint is connected to any FinishLine startPoint
        Collider2D[] colliders = Physics2D.OverlapBoxAll(endPoint.bounds.center, endPoint.bounds.size, 0f);

        foreach (Collider2D collider in colliders)
        {
            Road road = collider.GetComponent<Road>();
            if (road != null && road.isFinishLine && road.startPointConnectedTo(this.endPoint))
            {
                return true;
            }
        }

        return false;
    }

    private bool startPointConnectedTo(BoxCollider2D otherStartPoint)
    {
        // Check if this startPoint is connected to the provided startPoint
        return Physics2D.OverlapBox(startPoint.bounds.center, startPoint.bounds.size, 0f) == otherStartPoint;
    }

    private bool endPointConnectedTo(BoxCollider2D otherEndPoint)
    {
        // Check if this endPoint is connected to the provided endPoint
        return Physics2D.OverlapBox(endPoint.bounds.center, endPoint.bounds.size, 0f) == otherEndPoint;
    }
}

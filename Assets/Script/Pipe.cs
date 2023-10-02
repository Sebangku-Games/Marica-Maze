using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool IsFilled = false;
    public bool IsStartLine;
    public bool IsFinishLine;

    private const int maxRotation = 3;
    private const int rotationMultiplier = 90;

    private int rotation = 0;

    private SpriteRenderer emptySprite;
    private SpriteRenderer filledSprite;

    private void Start()
    {
        emptySprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        filledSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        UpdateFilled();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse click hits this pipe
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null && hitCollider.transform == transform)
            {
                if (!IsStartLine && !IsFinishLine)
                {
                    Rotate();
                }
            }
        }
    }

    public void Rotate()
    {
        rotation = (rotation + 1) % (maxRotation + 1);
        transform.eulerAngles = new Vector3(0, 0, rotation * rotationMultiplier);
    }

    public void UpdateFilled()
    {
        // If this pipe is a start line, always set IsFilled to true
        if (IsStartLine)
        {
            IsFilled = true;
        }

        emptySprite.gameObject.SetActive(!IsFilled);
        filledSprite.gameObject.SetActive(IsFilled);
    }

    // Mengatasi tabrakan dengan pipa lain
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Road") && other.GetComponent<Pipe>() != null)
        {
            Pipe pipe = other.GetComponent<Pipe>();
            if (pipe != null && pipe.IsFilled && !IsStartLine && !IsFinishLine)
            {
                // Ketika pipa bersentuhan dengan pipa lain yang sudah terisi, set IsFilled menjadi true
                IsFilled = true;
                UpdateFilled();
                Debug.Log("Pipa terhubung dengan pipa yang sudah terisi.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Road") && other.GetComponent<Pipe>() != null)
        {
            Pipe pipe = other.GetComponent<Pipe>();
            if (pipe != null && !IsStartLine && !IsFinishLine)
            {
                // Ketika pipa keluar dari collider, jika tidak dalam kontak dengan pipa lain, set IsFilled menjadi false
                IsFilled = false;
                UpdateFilled();
                Debug.Log("Pipa keluar dari collider dan set IsFilled menjadi false.");
            }
        }
    }
}

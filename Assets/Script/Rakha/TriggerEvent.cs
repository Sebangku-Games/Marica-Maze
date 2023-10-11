using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [Header("Main Settings")]
    public UnityEvent triggerEvent;
    public bool destroyTrigger;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Road road;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        road = GetComponentInParent<Road>();
    }

    public void InvokeTrigger(){
        triggerEvent.Invoke();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D is called from " + this.gameObject.tag + " to " + other.gameObject.tag);
        Road road = other.GetComponentInParent<Road>();
        if (road != null)
        {
            if (other.CompareTag("EndPoint"))
            {
                road.isStartPointConnectedToAnyEndPoint = true;
                road.UpdateFilled();
            }
            else if (other.CompareTag("StartPoint"))
            {
                road.isEndPointConnectedToAnyStartPoint = true;
                road.UpdateFilled();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D is called from " + this.gameObject.tag + " to " + other.gameObject.tag);
        Road road = other.GetComponentInParent<Road>();
        if (road != null)
        {
            if (other.CompareTag("EndPoint"))
            {
                road.isStartPointConnectedToAnyEndPoint = false;
                road.UpdateFilled();
            }
            else if (other.CompareTag("StartPoint"))
            {
                road.isEndPointConnectedToAnyStartPoint = false;
                road.UpdateFilled();
            }
        }
    }

}

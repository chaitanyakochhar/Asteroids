using UnityEngine;
using System.Collections;
using System;

public class Carrier : MonoBehaviour
{

    public float oneWayDuration = 1f;
    public bool isGiving { get; set; }
    public bool Going { get; set; }
    public bool Done { get; set; }

    public GameObject givingThis { get; set; }
    public Vector3 startPoint { get; set; }
    private Vector3 destination;
    private GameObject instancedObject;

    private void GiveObject(GameObject receiver)
    {
        instancedObject.transform.SetParent(receiver.transform);
    }

    private void CollectObject(GameObject takingThis)
    {
        takingThis.transform.SetParent(transform);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(isGiving)
        {
            GiveObject(other.gameObject);
        }
        else
        {
            CollectObject(other.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            if (isGiving)
            {
                Done = true;
                GiveObject(collision.gameObject);
            }
            else
            {
                Done = true;
                CollectObject(collision.gameObject);
            }
        }
    }

    public void GoToAndReturn(Vector3 destination)
    {
        if(isGiving)
        {
            instancedObject = Instantiate(givingThis, transform.position, Quaternion.identity) as GameObject;
            instancedObject.transform.parent = transform;
        }
        Done = false;
        Going = true;
        StartCoroutine(GoAndReturnCR(destination, startPoint));
    }

    private IEnumerator GoAndReturnCR(Vector3 to, Vector3 from)
    {
        float startTime = Time.time;
        float elapsed = Time.time - startTime;
        while(elapsed<=oneWayDuration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / oneWayDuration);
            elapsed = Time.time - startTime;
            yield return null;
        }
        startTime = Time.time;
        elapsed = Time.time - startTime;
        Going = false;
        while (elapsed <= oneWayDuration)
        {
            transform.position = Vector3.Lerp(to, from, elapsed / oneWayDuration);
            elapsed = Time.time - startTime;
            yield return null;
        }
        
    } 
}

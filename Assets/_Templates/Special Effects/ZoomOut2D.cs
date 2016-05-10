using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(Authenticator))]
public class ZoomOut2D : Effect
{
    public bool START_EFFECT_ON_START = false;
    public float initialDelay = 0;
    public float duration;
    public float viewPortStart;
    public float viewPortEnd;
    public GameObject lerpDestination;

    private float startTime;
    private Camera cam;
    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main;
        startPosition = cam.transform.position;
        if(START_EFFECT_ON_START)
        {
            StartEffect();
        }
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);
        startTime = Time.time;
        while((Time.time - startTime)<=duration)
        {
            cam.orthographicSize = Mathf.Lerp(viewPortStart, viewPortEnd, (Time.time - startTime) / duration);
            cam.transform.position = Vector3.Lerp(startPosition, lerpDestination.transform.position, (Time.time - startTime) / duration);
            yield return null;
        }
        GetComponent<Authenticator>().isAuthenticated = true;

    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        StopAllCoroutines();
    }
}

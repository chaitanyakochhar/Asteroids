using UnityEngine;
using System.Collections;

public class BackgroundLerp : MonoBehaviour
{

    public float lerpDistance = -182f;
    public float levelDuration = 60f;
    private float startTime;
    private Vector3 initial;
    private Vector3 finalDestination;
    // Use this for initialization
    void Start()
    {
        initial = transform.position;
        finalDestination = transform.position;
        finalDestination.x += lerpDistance;
        StartCoroutine(MoveBackground());
    }

    private IEnumerator MoveBackground()
    {
        startTime = Time.time;
        while ((Time.time - startTime) <= levelDuration)
        {
            transform.position = Vector3.Lerp(initial, finalDestination, (Time.time - startTime) / levelDuration);
            yield return null;
        }
        yield return null;
    }
}

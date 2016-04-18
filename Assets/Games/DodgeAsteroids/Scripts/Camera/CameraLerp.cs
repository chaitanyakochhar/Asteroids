using UnityEngine;
using System.Collections;

public class CameraLerp : MonoBehaviour
{
    public GameObject destination;
    public float lerpSeconds;

    private bool moved = false;
    public void OnTriggerEnter(Collider other)
    {
        if (!moved)
        {
            print("Moving camera");
            moved = true;
            StartCoroutine(MoveCamera());
            Vector3 newPoint = transform.position;
            newPoint.x += 2;
            NavMeshHit hit;
            NavMesh.SamplePosition(newPoint, out hit, 2, 1);
            other.GetComponent<NavMeshAgent>().SetDestination(hit.position);
        }
    }

    private IEnumerator MoveCamera()
    {
        float startTime = Time.time;
        while ((Time.time - startTime) <= lerpSeconds)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, destination.transform.position, (Time.time - startTime) / lerpSeconds);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}

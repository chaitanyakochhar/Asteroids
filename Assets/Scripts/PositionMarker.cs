using UnityEngine;
using System.Collections;

public class PositionMarker : MonoBehaviour
{
    public void ToggleMarker(bool visible)
    {
        GetComponent<SpriteRenderer>().enabled = visible;
    }
    public void MoveMarker(Vector3 destination)
    {
        transform.position = destination;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ToggleMarker(false);
        }
    }
}

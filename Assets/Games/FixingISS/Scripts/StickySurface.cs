using UnityEngine;
using System.Collections;
using FixingISSGame;
public class StickySurface : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sticker>() != null)
        {
            other.GetComponent<Sticker>().onStickySurface = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Sticker>() != null)
        {
            other.GetComponent<Sticker>().onStickySurface = false;
        }

    }
}

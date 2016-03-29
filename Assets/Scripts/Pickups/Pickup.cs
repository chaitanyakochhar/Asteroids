using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    private static int counter = 1;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

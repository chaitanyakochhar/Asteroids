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
            GameObject alienUI = GameObject.Find("Alien " + counter);
            Color c = alienUI.GetComponent<Image>().color;
            c.a = 255;
            alienUI.GetComponent<Image>().color = c;
            counter++;
            Destroy(gameObject);
        }
    }
}

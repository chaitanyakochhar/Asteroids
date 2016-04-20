using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour
{

    private float interval;
    private Renderer r;
    // Use this for initialization
    void Start()
    {
        r = GetComponent<Renderer>();
        StartCoroutine(Twinkle());
    }

    private IEnumerator Twinkle()
    {
        while (true)
        {
            interval = Random.Range(0.5f, 1f);
            r.enabled = true;
            yield return new WaitForSeconds(0.3f);
            r.enabled = false;
            yield return new WaitForSeconds(interval);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Bouncy : MonoBehaviour
{
    public float animationDuration = 2f;
    public Vector3 normal = new Vector3(1, 1, 1);
    public Vector3 large = new Vector3(2, 2, 1);

    private float startTime;

    private IEnumerator GrowShrinkAnimation()
    {
        while(true)
        {
            startTime = Time.time;
            while((Time.time - startTime) <= animationDuration / 2)
            {
                transform.localScale = Vector3.Lerp(normal, large, (Time.time - startTime) / (animationDuration / 2));
                yield return null;
            }
            startTime = Time.time;
            while ((Time.time - startTime) <= animationDuration / 2)
            {
                transform.localScale = Vector3.Lerp(large, normal, (Time.time - startTime) / (animationDuration / 2));
                yield return null;
            }
            yield return null;
        }
    }

    public void StartBouncy()
    {
        StartCoroutine(GrowShrinkAnimation());
    }

}

using UnityEngine;
using System.Collections;

public class ShakeLerp : MonoBehaviour {

    public float OscillationCount = 3f;
    public float intervalBetweenOscillations = 1f;


	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Oscillate());
	}
	
    private IEnumerator Oscillate()
    {
        while (true)
        {
            float startTime = Time.time;
            float pingPong; 
            while ((Time.time - startTime)<= OscillationCount)
            {
                pingPong = -1 + Mathf.PingPong(Time.time, 2f);
                transform.Rotate(Vector3.back, 1f * pingPong);
                yield return null;
            }
            yield return null;
        }
    }
}

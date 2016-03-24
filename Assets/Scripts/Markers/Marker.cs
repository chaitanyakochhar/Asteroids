using UnityEngine;
using System.Collections;

public class Marker : MonoBehaviour {

	// Use this for initialization
	public void Start ()
    {
        GetComponent<SpriteRenderer>().enabled = false;
	}
}

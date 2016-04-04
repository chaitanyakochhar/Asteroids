using UnityEngine;
using System.Collections;

public class ExitPageServerCall : MonoBehaviour {

    InterAppCommunicationManager i;

	// Use this for initialization
	void Start ()
    {
        i = GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>();
        i.CallbackSmartyPalNativeApp(i.gameName, i.result);
	}
	
}

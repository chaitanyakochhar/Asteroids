using UnityEngine;
using System.Collections;

public class StartMarker : Marker
{
    public GameObject player;

    new public void Start()
    {
        //base.Start();
        Instantiate(player);
    }

}

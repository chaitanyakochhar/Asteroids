using UnityEngine;
using System.Collections;

public class StartMarker : Marker
{
    public GameObject player;

    new public void Start()
    {
        //base.Start();
        if(player!=null)
        Instantiate(player);
    }

}

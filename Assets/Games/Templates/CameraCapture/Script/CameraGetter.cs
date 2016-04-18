using UnityEngine;
using System.Collections;
using System;

public class CameraGetter : MonoBehaviour
{
    public int numberOfPlayers = 4;
    private Texture2D[] textures;


    public void Start() 
    {
        textures = new Texture2D[numberOfPlayers];
        for (int i = 1; i <= numberOfPlayers; i++)
        {
            bool test = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>().imagesCaptured.TryGetValue("Player " + i, out textures[i - 1]);
            print(test);
            if (test)
            {
                GameObject.Find("Player " + i).GetComponent<Renderer>().material.mainTexture = textures[i - 1];
                textures[i - 1].Apply();
            }
        }
    }


}

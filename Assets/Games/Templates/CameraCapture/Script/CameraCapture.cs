﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class CameraCapture : MonoBehaviour
{

    public GameObject[] captureSurfaces;
    public GameObject overlay;
    public Vector2 overlayOffset;

    //Data Manager Object
    public PlayerDataManager playerDataManager;

    //Webcam business
    private WebCamDevice device;
    private CameraState2 state = CameraState2.START_CAMERA;
    private int i = 0;

    //Textures
    private WebCamTexture webcamTexture;
    private Texture2D capturedTexture;

    // Use this for initialization
    void Start()
    {

        playerDataManager = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
        foreach (WebCamDevice d in WebCamTexture.devices)
        {
            if (d.isFrontFacing)
            {
                device = d;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CameraState2.IDLE:
                {
                    break;
                }
            case CameraState2.START_CAMERA:
                {
                    if (i < captureSurfaces.Length)
                    {
                        webcamTexture = GetWebcamStream(captureSurfaces[i]);
                    }
                    state = CameraState2.IDLE;
                    break;
                }

            case CameraState2.TAKING_PICTURE:
                {
                    TakePicture();
                    state = CameraState2.CHOOSE_NEXT_OBJECT;
                    break;
                }
            case CameraState2.CHOOSE_NEXT_OBJECT:
                {
                    playerDataManager.imagesCaptured.Add("Player " + i, capturedTexture);
                    state = CameraState2.START_CAMERA;
                    break;
                }
        }
    }

    public void ButtonClicker()
    {
        state = CameraState2.TAKING_PICTURE;
    }

    private WebCamTexture GetWebcamStream(GameObject renderTarget)
    {
        WebCamTexture t = new WebCamTexture(device.name);
        Renderer r = renderTarget.GetComponent<Renderer>();
        r.material.mainTexture = t;
        t.Play();
        return t;
    }

    private void TakePicture()
    {
        if (i < captureSurfaces.Length)
        {
            capturedTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
            capturedTexture.SetPixels(webcamTexture.GetPixels());
            webcamTexture.Stop();
            i++;
        }
    }

}
using UnityEngine;
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
        if (GameObject.Find("Data Manager") != null)
            playerDataManager = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
        foreach (WebCamDevice d in WebCamTexture.devices)
        {
            if (d.isFrontFacing)
            {
                device = d;
            }
        }
        foreach (GameObject surface in captureSurfaces)
        {
            if (surface.GetComponent<Authenticator>() == null)
            {
                surface.AddComponent<Authenticator>();
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
                    if (playerDataManager != null)
                        playerDataManager.AddData<Texture2D>(playerDataManager.imagesCaptured, captureSurfaces[i - 1].name, capturedTexture);
                    captureSurfaces[i - 1].GetComponent<Authenticator>().isAuthenticated = true;
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
        if (renderTarget.GetComponent<RectTransform>() != null)
        {
            RawImage r = renderTarget.GetComponent<RawImage>();
            r.texture = t;
        }
        else
        {
            Renderer r = renderTarget.GetComponent<Renderer>();
            r.material.mainTexture = t;
        }
        t.Play();
        return t;
    }

    private void TakePicture()
    {
        if (i < captureSurfaces.Length)
        {
            capturedTexture = new Texture2D(webcamTexture.width, webcamTexture.height);
            capturedTexture.SetPixels(webcamTexture.GetPixels());
            capturedTexture.Apply();
            webcamTexture.Stop();
            SetPicture(capturedTexture, captureSurfaces[i]);

            i++;
        }
    }

    private void SetPicture(Texture2D picture, GameObject surface)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            surface.transform.Rotate(new Vector3(0, 0, 180));
        }
        if (surface.GetComponent<RectTransform>() != null)
        {
            surface.GetComponent<RawImage>().texture = picture as Texture2D;
        }
        else
        {
            surface.GetComponent<Renderer>().material.mainTexture = picture;
        }
    }

}

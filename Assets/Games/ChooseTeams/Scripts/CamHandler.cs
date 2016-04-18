using UnityEngine;
using System.Collections;
using System.IO;

public class CamHandler : MonoBehaviour
{
    public GameObject pilot;
    public GameObject scientist;
    private WebCamDevice frontFacing;
    private Texture2D pilotTexture;
    private Texture2D scientistTexture;

    private CameraState cameraState = CameraState.P1;
    private WebCamTexture activeTexture;

    public void Start()
    {
        foreach (WebCamDevice d in WebCamTexture.devices)
        {
            if (d.isFrontFacing)
            {
                frontFacing = d;
            }
        }

        activeTexture = GetWebcamStream(pilot);
    }

    public WebCamTexture GetWebcamStream(GameObject renderTarget)
    {
        WebCamTexture t = new WebCamTexture(frontFacing.name);
        Renderer r = renderTarget.GetComponent<Renderer>();
        r.material.mainTexture = t;
        t.Play();
        return t;
    }

    public void TakePicture()
    {
        switch (cameraState)
        {
            case CameraState.P1:
                {
                    pilotTexture = new Texture2D(activeTexture.width, activeTexture.height);
                    pilotTexture.SetPixels(activeTexture.GetPixels());
                    activeTexture.Stop();
                    activeTexture = GetWebcamStream(scientist);
                    cameraState = CameraState.P2;
                    break;
                }
            case CameraState.P2:
                {
                    scientistTexture = new Texture2D(activeTexture.width, activeTexture.height);
                    scientistTexture.SetPixels(activeTexture.GetPixels());
                    activeTexture.Stop();
                    cameraState = CameraState.NONE;
                    break;
                }
        }

    }

    public void WritePhotosAsPNG()
    {
        File.WriteAllBytes("C:\\pilot.png", pilotTexture.EncodeToPNG());
        File.WriteAllBytes("C:\\scientist.png", scientistTexture.EncodeToPNG());
    }
}

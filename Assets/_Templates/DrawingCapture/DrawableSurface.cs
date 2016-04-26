using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawableSurface : MonoBehaviour
{
    public Samples NumSamples = Samples.Samples4;
    public float brushRadius = 1000f;
    public Color colorBeingUsed = Color.black;

    private Texture2D tNew;
    private Vector2 lastPoint;
    private Vector2 currentPoint;
    public bool EraserMode { get; set; }
    public bool isTouchDevice { get; set; }
    private RuntimePlatform platform;

    void Start()
    {
        EraserMode = false;
        platform = Application.platform;
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            isTouchDevice = true;
        }
        else
        {
            isTouchDevice = false;
        }
        Texture2D t = GetComponent<Renderer>().material.mainTexture as Texture2D;
        tNew = Instantiate(t) as Texture2D;
        transform.GetComponent<Renderer>().material.mainTexture = tNew;
        ClearOut();
    }

    public void Update()
    {
        if (isTouchDevice)
        {

        }
        else ClickHandler();
    }

    private void ClickHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Command c = Command.createCommandWithHitObjectReference(Input.mousePosition, out hit);
            if (c == null)
            {
                print("Fail");
            }
            else
            {
                lastPoint = hit.textureCoord;
                lastPoint.x *= tNew.width;
                lastPoint.y *= tNew.height;
                if (EraserMode)
                {
                    tNew = Drawing.Paint(lastPoint, brushRadius, Color.clear, 10, tNew);

                }
                else
                {
                    tNew = Drawing.Paint(lastPoint, brushRadius, colorBeingUsed, 10, tNew);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Command c = Command.createCommandWithHitObjectReference(Input.mousePosition, out hit);
            if (c == null)
            {
                print("Fail");
            }
            else
            {
                print(hit.textureCoord);
                currentPoint = hit.textureCoord;
                currentPoint.x *= tNew.width;
                currentPoint.y *= tNew.height;
                if (EraserMode)
                {
                    tNew = Drawing.PaintLine(lastPoint, currentPoint, brushRadius, Color.clear, 10, tNew);

                }
                else
                {
                    tNew = Drawing.PaintLine(lastPoint, currentPoint, brushRadius, colorBeingUsed, 10, tNew);
                }
                tNew.Apply();
                lastPoint = currentPoint;
            }
        }

    }

    private void TouchHandler()
    {


        foreach (Touch t in Input.touches)
        {
            RaycastHit hit;
            Command c = Command.createCommandWithHitObjectReference(t.position, out hit);
            if (c == null)
            {
                print("Fail");
            }
            else
            {
                switch (t.phase)
                {
                    case TouchPhase.Began:
                        {
                            lastPoint = hit.textureCoord;
                            lastPoint.x *= tNew.width;
                            lastPoint.y *= tNew.height;
                            if (EraserMode)
                            {
                                tNew = Drawing.Paint(lastPoint, brushRadius, Color.clear, 10, tNew);

                            }
                            else
                            {
                                tNew = Drawing.Paint(lastPoint, brushRadius, colorBeingUsed, 10, tNew);
                            }
                            tNew.Apply();
                            break;
                        }
                    case TouchPhase.Moved:
                        {
                            print(hit.textureCoord);
                            currentPoint = hit.textureCoord;
                            currentPoint.x *= tNew.width;
                            currentPoint.y *= tNew.height;
                            if (EraserMode)
                            {
                                tNew = Drawing.PaintLine(lastPoint, currentPoint, brushRadius, Color.clear, 10, tNew);

                            }
                            else
                            {
                                tNew = Drawing.PaintLine(lastPoint, currentPoint, brushRadius, colorBeingUsed, 10, tNew);
                            }
                            tNew.Apply();
                            lastPoint = currentPoint;
                            break;
                        }
                    case TouchPhase.Ended:
                        {
                            break;
                        }
                }
            }
        }
    }

    private void Paint(Vector2 textureCoord)
    {
        textureCoord.x *= tNew.width;
        textureCoord.y *= tNew.height;
        tNew.SetPixel((int)textureCoord.x, (int)textureCoord.y, Color.black);
        tNew.Apply();
    }

    private Vector2[] calculateIntermediates(Vector2 startPoint, Vector2 endPoint, int samplePoints = 2)
    {
        Vector2[] intermediates = new Vector2[samplePoints];
        for (int i = 0; i < samplePoints; i++)
        {
            intermediates[i] = Vector2.Lerp(startPoint, endPoint, (i + 1) / samplePoints);
        }
        return intermediates;
    }

    private void ClearOut()
    {
        Color[] blanks = new Color[tNew.width * tNew.height];
        for (int i = 0; i < blanks.Length; i++)
        {
            blanks[i] = Color.clear;
        }
        tNew.SetPixels(blanks);
        tNew.Apply();
    }

}

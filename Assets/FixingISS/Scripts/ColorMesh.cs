﻿using UnityEngine;
using System.Collections;

public class ColorMesh : MonoBehaviour
{
    Color[] c;
    Texture2D tNew;
    public void Start()
    {
        Texture2D t = transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
        Texture2D tNew = Instantiate(t) as Texture2D;
        transform.GetComponent<Renderer>().material.mainTexture = tNew;
    }

    public void RaycastListener(Command c, RaycastHit hitInfo)
    {
        Vector2 point = hitInfo.textureCoord;
        Texture2D t = transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
        ColorTexture(point, t, 50, 50);
        print(GetColorPercentage(t, Color.red));

    }

    private Color[] createColorArray(int length, Color defaultColor)
    {
        Color[] c = new Color[length];
        for (int x = 0; x < length; x++)
        {
            c[x] = defaultColor;
        }
        return c;
    }

    private void ColorTexture(Vector2 point, Texture2D textureToPaint, int widthOfBrush = 10, int heightOfBrush = 10)
    {
        point.x *= textureToPaint.width;
        point.y *= textureToPaint.height;
        if ((int)point.x + widthOfBrush > textureToPaint.width)
        {
            int offset = textureToPaint.width - widthOfBrush - (int)point.x;
            widthOfBrush += offset;
        }

        if ((int)point.y + heightOfBrush > textureToPaint.height)
        {
            int offset = textureToPaint.height - heightOfBrush - (int)point.y;
            heightOfBrush += offset;
        }

        textureToPaint.SetPixels((int)point.x, (int)point.y, widthOfBrush, heightOfBrush, createColorArray(widthOfBrush * heightOfBrush, Color.red));
        textureToPaint.Apply();
    }

    private float GetColorPercentage(Texture2D textureToCheck, Color targetColor)
    {
        float positivePixels = 0;
        float totalPixels = textureToCheck.width * textureToCheck.height;

        for(int i = 0; i <textureToCheck.width; i++)
            for(int j = 0; j < textureToCheck.height; j++)
            {
                if (textureToCheck.GetPixel(i, j) == targetColor)
                {
                    positivePixels++;
                }
            }
        return positivePixels * 100 / totalPixels;
    }
}
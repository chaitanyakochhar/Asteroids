using UnityEngine;
using System.Collections;
namespace FixingISSGame
{
    public class ColorMesh : MonoBehaviour
    {

        public float passPercentage = 90f;

        private float currentPercentage = 0f;
        private SpriteRenderer alert;

        public void Start()
        {
            Texture2D t = transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
            Texture2D tNew = Instantiate(t) as Texture2D;
            transform.GetComponent<Renderer>().material.mainTexture = tNew;
            alert = transform.GetChild(0).GetComponent<SpriteRenderer>();
            alert.GetComponent<Bouncy>().StartBouncy();
            alert.enabled = false;
        }

        public void RaycastListener(Vector2 textureCoord, Color targetColor)
        {
            Vector2 point = textureCoord;
            Texture2D t = transform.GetComponent<Renderer>().material.mainTexture as Texture2D;
            ColorTexture(point, t, targetColor, 50, 50);
            currentPercentage = GetColorPercentage(t, targetColor);

        }

        public bool Evaluate()
        {
            print(currentPercentage);
            if (passPercentage <= currentPercentage)
            {
                return true;
            }
            else
            {
                StartCoroutine(bouncyEnable());
                return false;
            }
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

        private void ColorTexture(Vector2 point, Texture2D textureToPaint, Color targetColor, int widthOfBrush = 10, int heightOfBrush = 10)
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

            textureToPaint.SetPixels((int)point.x, (int)point.y, widthOfBrush, heightOfBrush, createColorArray(widthOfBrush * heightOfBrush, targetColor));
            textureToPaint.Apply();
        }

        private float GetColorPercentage(Texture2D textureToCheck, Color targetColor)
        {
            float positivePixels = 0;
            float totalPixels = textureToCheck.width * textureToCheck.height;

            for (int i = 0; i < textureToCheck.width; i++)
                for (int j = 0; j < textureToCheck.height; j++)
                {
                    if (textureToCheck.GetPixel(i, j) == targetColor)
                    {
                        positivePixels++;
                    }
                }
            return positivePixels * 100 / totalPixels;
        }

        private IEnumerator bouncyEnable()
        {
            alert.enabled = true;
            yield return new WaitForSeconds(4);
            alert.enabled = false;
        }
   
    }
}
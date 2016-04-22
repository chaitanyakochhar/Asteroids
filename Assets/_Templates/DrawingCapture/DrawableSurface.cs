using UnityEngine;
using System.Collections;

public class DrawableSurface : MonoBehaviour
{
    public float brushRadius = 10f;
    public Color colorBeingUsed = Color.black;
    private Texture2D tNew;


    void Start()
    {
        Texture2D t = GetComponent<Renderer>().material.mainTexture as Texture2D;
        tNew = Instantiate(t) as Texture2D;
        transform.GetComponent<Renderer>().material.mainTexture = tNew;
    }

    public void Update()
    {
        ClickHandler();
    }

    private void ClickHandler()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Command c = Command.createCommandWithHitObjectReferenceIgnoreUI(Input.mousePosition, out hit);
            if (c == null)
            {
                print("Fail");
            }
            if (c != null)
                print(hit.textureCoord);
            StartCoroutine(CirclePaint(hit.textureCoord, brushRadius));

        }
    }

    private void TouchHandler()
    {

    }

    private void Paint(Vector2 textureCoord)
    {
        textureCoord.x *= tNew.width;
        textureCoord.y *= tNew.height;
        tNew.SetPixel((int)textureCoord.x, (int)textureCoord.y, Color.black);
        tNew.Apply();
    }

    private IEnumerator CirclePaint(Vector2 textureCoord, float radius)
    {
        textureCoord.x *= tNew.width;
        textureCoord.y *= tNew.height;
        float dist;
        for (int x = 0;x<tNew.width;x++)
            for(int y = 0; y < tNew.height; y++)
            {
                float diffX = x - textureCoord.x;
                float diffY = y - textureCoord.y;

                dist = Mathf.Sqrt((diffX * diffX) + (diffY * diffY));
                if(dist<=radius)
                {
                    tNew.SetPixel(x, y, colorBeingUsed);
                }
            }
        tNew.Apply();
        yield return null;
    }


}

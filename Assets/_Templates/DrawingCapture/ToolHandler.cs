using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ToolHandler : MonoBehaviour
{
    private DrawableSurface d;
    public float eraserRadius = 50f;

    private float brushRadius;

    public void Start()
    {
        d = GameObject.Find("Drawable Surface").GetComponent<DrawableSurface>();
        brushRadius = d.brushRadius;
    }

    public void Eraser()
    {
        d.EraserMode = true;
        d.brushRadius = eraserRadius;
    }

    public void Pencil()
    {
        d.EraserMode = false;
        d.brushRadius = brushRadius;
    }
}

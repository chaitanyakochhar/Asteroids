using UnityEngine;
using UnityEngine.UI;

public class PaintHandler : MonoBehaviour {
    public void UpdateColor()
    {
        GameObject.Find("Drawable Surface").GetComponent<DrawableSurface>().colorBeingUsed = GetComponent<Image>().color;
    }
}

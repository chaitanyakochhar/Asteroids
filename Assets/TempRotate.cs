using UnityEngine;
using System.Collections;

public class TempRotate : MonoBehaviour {
    public void TurnBy90(string direction)
    {
        switch(direction)
        {
            case "c":
                {
                    transform.Rotate(new Vector3(0, 0, 45));
                    break;
                }
            case "ac":
                {
                    transform.Rotate(new Vector3(0, 0, -45));
                    break;
                }
        }
    }
}

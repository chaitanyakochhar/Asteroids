using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Command
{

    public Vector2 screenPoint { get; set; }
    public Vector3 worldPoint { get; set; }

    private Command(Vector2 sPoint, Vector3 wPoint)
    {
        screenPoint = sPoint;
        worldPoint = wPoint;
    }

    public static Command createCommand(Vector2 destination)
    {
        Ray r = Camera.main.ScreenPointToRay(destination);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(r, out hit);
        if(hasHit && !EventSystem.current.IsPointerOverGameObject())
        {
            return new Command(destination, hit.point);
        }
        return null;
    }
    
}

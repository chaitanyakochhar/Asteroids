using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;


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
        bool hasHitUI = PointIsOverUI(destination);
        if (hasHitUI)
        {
            Debug.Log("I have hit UI");
        }
        if(hasHit && !hasHitUI)
        {
            return new Command(destination, hit.point);
        }
        return null;
    }

    public static Command createCommandWithHitObjectReference(Vector2 destination, out RaycastHit hit, bool ignoreUI=true)
    {
        bool hasHitUI = false;
        if(ignoreUI==true)
        {
            hasHitUI = PointIsOverUI(destination);
        }
        Ray r = Camera.main.ScreenPointToRay(destination);
        bool hasHit = Physics.Raycast(r, out hit);
        if (hasHit && !hasHitUI)
        {
            return new Command(destination, hit.point);
        }
        return null;
    }

    public static Command createCommandWithHitObjectReferenceIgnoreUI(Vector2 destination, out RaycastHit hit)
    {
        Ray r = Camera.main.ScreenPointToRay(destination);
        bool hasHit = Physics.Raycast(r, out hit);
        if (hasHit)// && !EventSystem.current.IsPointerOverGameObject())
        {
            return new Command(destination, hit.point);
        }
        return null;
    }

    public static Command createCommandWithoutRaycast(Vector2 destination, bool ignoreUI = false)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(destination);
        if (!ignoreUI)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                return new Command(destination, worldPoint);
            }
        }
        else
        {
            return new Command(destination, worldPoint);
        }

        return null;
    }

    private static List<RaycastResult> tempResult = new List<RaycastResult>();

    private static bool PointIsOverUI(Vector2 location)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = location;
        tempResult.Clear();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, tempResult);
        return tempResult.Count > 0;
    }
    
}

﻿using UnityEngine;
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

    public static Command createCommandWithHitObjectReference(Vector2 destination, out RaycastHit hit, bool ignoreUI=true)
    {
        bool blockedByUIElement = false;
        if(ignoreUI==true)
        {
            blockedByUIElement = EventSystem.current.IsPointerOverGameObject();
        }
        Ray r = Camera.main.ScreenPointToRay(destination);
        bool hasHit = Physics.Raycast(r, out hit);
        if (hasHit && !blockedByUIElement)
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

    public static Command createCommandWithoutRaycast(Vector2 destination)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(destination);
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            return new Command(destination, worldPoint);
        }
        return null;
    }
    
}

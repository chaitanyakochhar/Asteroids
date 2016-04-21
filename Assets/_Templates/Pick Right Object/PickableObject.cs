using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Authenticator))]
[RequireComponent(typeof(Button))]
public class PickableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Listener listener;
    public PickObjectButtonSetting pickObjectButtonSetting = PickObjectButtonSetting.HOLD;
    public Color iconColor = Color.black;

    public void Start()
    {
        listener = GameObject.Find("Listener").GetComponent<Listener>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        listener.RegisterObject(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(pickObjectButtonSetting == PickObjectButtonSetting.HOLD)
        {
            listener.DeregisterObject(gameObject);
        }
    }
}

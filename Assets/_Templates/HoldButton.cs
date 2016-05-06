using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(Authenticator))]
public class HoldButton : Effect, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Bar;
    public float duration = 3f;

    private float startTime;
    private float elapsedTime;
    
    public override IEnumerator EffectCoroutine()
    {
        startTime = Time.time;
        elapsedTime = Time.time - startTime;
        while(elapsedTime<=duration)
        {
            elapsedTime = Time.time - startTime;
            if(Bar!=null && Bar.GetComponent<Image>()!=null)
            {
                Bar.GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            }
            yield return null;
        }
        GetComponent<Authenticator>().isAuthenticated = true;
        GetComponent<Button>().interactable = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        StartEffect();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Bar != null && Bar.GetComponent<Image>() != null && GetComponent<Authenticator>().isAuthenticated == false)
        {
            Bar.GetComponent<Image>().fillAmount = 0f;
        }
        StopEffect();
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        StopAllCoroutines();
    }
}

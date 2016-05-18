using UnityEngine;
using System.Collections;

public class CrossPlatformInput : MonoBehaviour
{

    public bool IgnoreUI = true;
    public bool requiresAuthentication = true;
    void Update()
    {
        if (requiresAuthentication)
        {
            if (GetComponent<Authenticator>().isAuthenticated)
            {
                MouseListener();
                TouchListener();
            }
        }
        else
        {
            MouseListener();
            TouchListener();
        }
    }
    
    private void MouseListener()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Command c = Command.createCommandWithHitObjectReferenceIgnoreUI(Input.mousePosition, out hit);
            if(c!=null)
            {
                if(hit.transform.GetComponent<AnimateOnClick>()!= null)
                {
                    print(hit.transform.name);
                    hit.transform.GetComponent<AnimateOnClick>().StartEffect();
                }
            }
        }
    }

    private void TouchListener()
    {
        if(Input.touchCount>0)
        {
            Touch t = Input.touches[0];
            switch(t.phase)
            {
                case TouchPhase.Began:
                    {
                        RaycastHit hit;
                        Command c = Command.createCommandWithHitObjectReferenceIgnoreUI(t.position, out hit);
                        if (c != null)
                        {
                            if (hit.transform.GetComponent<AnimateOnClick>() != null)
                            {
                                print(hit.transform.name);
                                hit.transform.GetComponent<AnimateOnClick>().StartEffect();
                            }
                        }
                        break;
                    }
            }
        }
    }
}

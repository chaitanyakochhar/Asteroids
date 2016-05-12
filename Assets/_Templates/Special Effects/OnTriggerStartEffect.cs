using UnityEngine;
using System.Collections;


public class OnTriggerStartEffect : MonoBehaviour
{
    public GameObject[] triggerThese;

    public void OnTriggerEnter(Collider other)
    {
        foreach(GameObject GO in triggerThese)
        {
            if(GO!=null)
            {
                if(GO.GetComponent<Effect>()!=null)
                {
                    GO.GetComponent<Effect>().StartEffect();
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject GO in triggerThese)
        {
            if (GO != null)
            {
                if (GO.GetComponent<Effect>() != null)
                {
                    GO.GetComponent<Effect>().StartEffect();
                }
            }
        }
    }
}

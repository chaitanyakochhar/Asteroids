using UnityEngine;
using System.Collections;

namespace FixingISSGame
{
    public class Slot : MonoBehaviour
    {

        public bool hasItem { get; set; }

        public void Start()
        {
            hasItem = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            print("Entered trigger, screw slotting in..");
            if(other.tag=="Instrument" && !hasItem)
            {
                if(other.GetComponent<Screw>()!=null || other.GetComponent<Panel>() != null)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.z = other.transform.position.z;
                    if (other.GetComponent<Screw>() != null)
                        other.transform.GetComponent<Screw>().ChangeState(newPosition);
                    else
                        other.transform.GetComponent<Panel>().ChangeState(newPosition);
                    GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }
}

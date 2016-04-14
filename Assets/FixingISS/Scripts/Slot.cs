using UnityEngine;
using System.Collections;

namespace FixingISSGame
{
    public class Slot : MonoBehaviour
    {

        public bool hasItem { get; set; }
        public string type;

        public void Start()
        {
            hasItem = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            print("Entered trigger, screw slotting in..");
            print(other.name);
            print(other.tag);
            print(hasItem);
            if (other.tag == "Instrument" && !hasItem)
            {
                switch (type)
                {
                    case "Screw":
                        {
                            if (other.GetComponent<Screw>() != null)
                            {
                                slotIntoPlace(other.gameObject);
                            }
                            break;
                        }
                    case "Panel":
                        {
                            if (other.GetComponent<Panel>() != null)
                            {
                                slotIntoPlace(other.gameObject);
                            }
                            break;
                        }
                }
               
            }
        }

        private void slotIntoPlace(GameObject other)
        {
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

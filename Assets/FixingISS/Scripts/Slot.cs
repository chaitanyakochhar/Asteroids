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
            print("bang");
            if(other.tag=="Instrument" && other.transform.GetComponent<Screw>() != null && !hasItem)
            {
                hasItem = true;
                Vector3 newPosition = transform.position;
                newPosition.z = other.transform.position.z;
                other.transform.GetComponent<Screw>().ChangeState(newPosition);
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}

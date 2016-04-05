using UnityEngine;
using System.Collections;

namespace FixingISSGame
{
    public class Instrument : MonoBehaviour
    {
        private bool hasBeenSelected = false;
        private int fingerID = -1;
        private Vector3 location;
        // Update is called once per frame


        public void ActivateInstrument(Command c, Touch t)
        {
            if (fingerID == -1)
            {
                fingerID = t.fingerId;
            }
        }
        public void MoveInstrument(Command c, Touch t)
        {
            if(fingerID == t.fingerId)
            {
                location = c.worldPoint;
                location.z = transform.position.z;
                transform.position = location;
            }
        }

        public void DeactivateInstrument(Command c, Touch t)
        {
            if(fingerID == t.fingerId)
            {
                fingerID = -1;
            }
        }

        private void RaycastOntoColorMesh(Touch t)
        {
            RaycastHit hit;
            Command c = Command.createCommandWithHitObjectReference(t.position, out hit);
            if(hit.transform.tag == "Paintable")
            {
                hit.transform.GetComponent<ColorMesh>().RaycastListener(c, hit);
            }
        }

    }
}

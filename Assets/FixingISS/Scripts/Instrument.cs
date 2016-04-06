using UnityEngine;
using System.Collections;

namespace FixingISSGame
{
    public class Instrument : MonoBehaviour
    {
        public Color colorToUse;
        public GameObject canPaintThis;

        protected bool hasBeenSelected = false;
        protected int fingerID = -1;
        protected Vector3 location;
        // Update is called once per frame

        public virtual void ActivateInstrument(Command c, Touch t)
        {
            if (fingerID == -1)
            {
                fingerID = t.fingerId;
            }
        }

        public virtual void MoveInstrument(Command c, Touch t)
        {
            if (fingerID == t.fingerId)
            {
                print("FingerID match for " + name);
                location = c.worldPoint;
                location.z = transform.position.z;
                transform.position = location;
                RaycastOntoColorMesh(t);
            }
        }
        public virtual void DeactivateInstrument(Command c, Touch t)
        {
            if (fingerID == t.fingerId)
            {
                fingerID = -1;
            }
        }
        private void RaycastOntoColorMesh(Touch t)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit) && canPaintThis!=null)
                if (hit.transform == canPaintThis.transform)
                {
                    hit.transform.GetComponent<ColorMesh>().RaycastListener(hit.textureCoord, colorToUse);
                }
        }

    }
}

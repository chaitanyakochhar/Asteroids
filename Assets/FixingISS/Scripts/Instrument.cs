using UnityEngine;
using System.Collections;
using System;

namespace FixingISSGame
{
    public class Instrument : Item
    {
        public Color colorToUse;
        public GameObject[] canPaintThis;
        protected Vector3 location;

        #region Item methods
        public override void Activate(Command c, Touch t)
        {
            if (fingerID == -1)
            {
                fingerID = t.fingerId;
            }
        }
        public override void Move(Command c, Touch t)
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
        public override void Deactivate(Command c, Touch t)
        {
            if (fingerID == t.fingerId)
            {
                fingerID = -1;
            }

        }
        #endregion

        private void RaycastOntoColorMesh(Touch t)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit) && canPaintThis != null)
            {
                foreach (GameObject paint in canPaintThis)
                {
                    if (hit.transform == paint.transform)
                    {
                        hit.transform.GetComponent<ColorMesh>().RaycastListener(hit.textureCoord, colorToUse);
                        break;
                    }
                }

            }
        }

        public override bool Evaluate()
        {
            return true;
        }
    }
}

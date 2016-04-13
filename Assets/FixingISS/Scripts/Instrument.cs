using UnityEngine;
using SpaceShooterGame;

namespace FixingISSGame
{
    public class Instrument : Item
    {
        public Color colorToUse;
        public GameObject[] canPaintThis;
        protected Vector3 location;
        protected Rotator2D rotator2d;
        protected Mover2D mover2d;

        public void Start()
        {
            rotator2d = GetComponent<Rotator2D>();
            mover2d = GetComponent<Mover2D>();
        }
        
        #region Item methods
        public override void Activate(Command c, Touch t)
        {
            if (fingerID == -1)
            {
                fingerID = t.fingerId;
                StopEffects();
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
                StartEffects();
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

        public override void StartEffects()
        {
            if(rotator2d!=null)
            {
                rotator2d.enabled = true;
                transform.rotation = Quaternion.identity;
            }
            if(mover2d!=null)
            {
                mover2d.Toggle(true);
            }
        }

        public override void StopEffects()
        {
            if (rotator2d != null)
            {
                rotator2d.enabled = false;
                transform.rotation = Quaternion.identity;
            }
            if (mover2d != null)
            {
                mover2d.Toggle(false);
            }
        }
    }
}

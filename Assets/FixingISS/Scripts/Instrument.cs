using UnityEngine;
using SpaceShooterGame;
using System;
using System.Collections;

namespace FixingISSGame
{
    public class Instrument : Item
    {
        public Color colorToUse;
        public GameObject[] canPaintThis;
        protected Vector3 location;

        public void Start()
        {
            rotator2d = GetComponent<Rotator2D>();
            mover2d = GetComponent<Mover2D>();
            source = GetComponent<AudioSource>();
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
                        PlaySound(ItemState.IN_PROGRESS);
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
            if (rotator2d != null)
            {
                rotator2d.enabled = true;
                transform.rotation = Quaternion.identity;
            }
            if (mover2d != null)
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

        public override void PlaySound(ItemState itemState)
        {
            switch (itemState)
            {
                case ItemState.IN_PROGRESS:
                    {
                        
                        if (inProgressSound != null && inProgressSound.Length > 0)
                        {
                            if (!source.isPlaying)
                                source.PlayOneShot(inProgressSound[UnityEngine.Random.Range(0, inProgressSound.Length - 1)]);
                        }
                        break;
                    }
            }
        }

        public override void ActivateTheseObjectsOnCompletion()
        {
            foreach(GameObject GO in activateOnCompletion)
            {
                GO.SetActive(true);
            }
        }

        protected override IEnumerator bouncyEnable()
        {
            throw new NotImplementedException();
        }
    }
}

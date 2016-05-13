using UnityEngine;
using SpaceShooterGame;
using System;
using System.Collections;

namespace FixingISSGame
{
    public class Screw : Item
    {
        public GameObject screwInsertedImage;
        public float numberOfTapsNeeded = 2;

        private Color startColor = Color.red;
        private Color endColor = Color.green;
        private int tapsMade = 0;
        private float fraction = 0;

        public void Start()
        {
            itemState = ItemState.LOOSE;
            rotator2d = GetComponent<Rotator2D>();
            mover2d = GetComponent<Mover2D>();
            source = GetComponent<AudioSource>();
            alert = transform.GetChild(0).gameObject;
            alert.SetActive(false);
        }

        public override void Activate(Command c, Touch t)
        {
            switch (itemState)
            {
                case ItemState.LOOSE:
                    {
                        if (fingerID == -1)
                        {
                            StopEffects();
                            PlaySound(itemState);
                            fingerID = t.fingerId;
                        }
                        break;
                    }
                case ItemState.IN_PROGRESS:
                    {
                        PlaySound(itemState);
                        TapToTighten();
                        break;
                    }
                case ItemState.DONE:
                    {
                        PlaySound(itemState);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        public override void Move(Command c, Touch t)
        {
            Vector3 location;
            if (fingerID == t.fingerId && itemState == ItemState.LOOSE)
            {
                print("FingerID match for " + name);
                location = c.worldPoint;
                location.z = transform.position.z;
                transform.position = location;
            }
        }
        public override void Deactivate(Command c, Touch t)
        {
            if (fingerID == t.fingerId)
            {
                fingerID = -1;
                if (itemState == ItemState.LOOSE)
                {
                    StartEffects();
                }
            }

        }
        public override bool Evaluate()
        {
            print(itemState);
            if (itemState == ItemState.DONE)
            {
                return true;
            }
            else
            {
                StartCoroutine(bouncyEnable());
                return false;
            }
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
                case ItemState.LOOSE:
                    {
                        if (activationSound != null)
                        {
                            source.PlayOneShot(activationSound);
                        }
                        break;
                    }
                case ItemState.IN_PROGRESS:
                    {
                        if (inProgressSound != null && inProgressSound.Length > 0)
                        {
                            source.PlayOneShot(inProgressSound[UnityEngine.Random.Range(0, inProgressSound.Length - 1)]);
                        }
                        break;
                    }
                case ItemState.DONE:
                    {
                        if (doneSound != null)
                        {
                            source.PlayOneShot(doneSound);
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

        public void TapToTighten()
        {
            if (tapsMade < numberOfTapsNeeded)
            {
                tapsMade++;
                fraction = tapsMade / numberOfTapsNeeded;
                GetComponent<SpriteRenderer>().color = ColorLerp(startColor, Color.yellow, endColor, fraction);
            }


            if (tapsMade == numberOfTapsNeeded)
            {
                itemState = ItemState.DONE;
                GetComponent<SpriteRenderer>().color = ColorLerp(startColor, Color.yellow, endColor, 1f);
            }

        }
        private Color ColorLerp(Color startColor, Color intermediateColor, Color endColor, float fraction)
        {
            float intermediateFraction;
            if (fraction <= 0.5)
            {
                intermediateFraction = fraction + 0.5f;
                return Color.Lerp(startColor, intermediateColor, intermediateFraction);
            }
            else
            {
                intermediateFraction = fraction - 0.5f;
                intermediateFraction *= 2f;
                return Color.Lerp(intermediateColor, endColor, intermediateFraction);
            }

        }
        public void ChangeState(Vector3 locationOfSlot)
        {
            transform.position = locationOfSlot;
            itemState = ItemState.IN_PROGRESS;
            GetComponent<SpriteRenderer>().sprite = screwInsertedImage.GetComponent<SpriteRenderer>().sprite;
            GetComponent<SpriteRenderer>().color = ColorLerp(startColor, Color.yellow, endColor, 0f);
            if(numberOfTapsNeeded==0)
            {
                GetComponent<SpriteRenderer>().color = endColor;
                itemState = ItemState.DONE;
            }
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        protected override IEnumerator bouncyEnable()
        {
            alert.SetActive(true);
            yield return new WaitForSeconds(4);
            alert.SetActive(false);
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace FixingISSGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    public class Screw : Item
    {
        public GameObject screwInsertedImage;
        public float numberOfTapsNeeded = 2;

        private Color startColor = Color.red;
        private Color endColor = Color.green;
        private Vector3 location;
        private int tapsMade = 0;
        private float fraction = 0;

        public void Start()
        {
            itemState = ItemState.LOOSE;
        }

        public override void Activate(Command c, Touch t)
        {


            switch (itemState)
            {
                case ItemState.LOOSE:
                    {
                        if(fingerID == -1)
                        {
                            fingerID = t.fingerId;
                        }
                        break;
                    }
                case ItemState.IN_PROGRESS:
                    {
                        TapToTighten();
                        break;
                    }
                case ItemState.DONE:
                    {
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
        }


    }
}

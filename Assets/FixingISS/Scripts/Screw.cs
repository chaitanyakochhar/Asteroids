using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace FixingISSGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    public class Screw : Instrument
    {

        public Sprite screwInsertedImage;
        public float numberOfTapsNeeded = 2;
        public ScrewState screwState = ScrewState.LOOSE;

        private Image progressBarImage;
        private Image progressBarBackgroundImage;
        private float tapsMade = 0;

        private Color startColor = Color.red;
        private Color endColor = Color.green;

        public void Start()
        {
            progressBarImage = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            progressBarBackgroundImage = transform.GetChild(0).GetChild(1).GetComponent<Image>();
            progressBarBackgroundImage.enabled = false;

        }

        #region Instrument Methods

        public override void ActivateInstrument(Command c, Touch t)
        {
            switch (screwState)
            {
                case ScrewState.LOOSE:
                    {
                        base.ActivateInstrument(c, t);
                        break;
                    }
                case ScrewState.IN_SLOT:
                    {
                        print("Screw in Slot");
                        TapToTighten();
                        break;
                    }
            }
        }
        public override void MoveInstrument(Command c, Touch t)
        {

            if (t.fingerId == fingerID && screwState == ScrewState.LOOSE)
            {
                location = c.worldPoint;
                location.z = transform.position.z;
                transform.position = location;
            }

        }
        public override void DeactivateInstrument(Command c, Touch t)
        {
            base.DeactivateInstrument(c, t);
        }

        #endregion


        public void TapToTighten()
        {
            if (tapsMade < numberOfTapsNeeded)
            {
                tapsMade++;
                float fraction = tapsMade / numberOfTapsNeeded;
                GetComponent<SpriteRenderer>().color = ColorLerp(startColor, Color.yellow, endColor, fraction);
            }


            if(tapsMade == numberOfTapsNeeded)
            {
                progressBarBackgroundImage.enabled = false;
                screwState = ScrewState.TIGHT;
                GetComponent<SpriteRenderer>().color = ColorLerp(startColor, Color.yellow, endColor, 1f);
            }
             
        }

        public void ChangeState(Vector3 locationOfSlot)
        {
            transform.position = locationOfSlot;
            screwState = ScrewState.IN_SLOT;
            GetComponent<SpriteRenderer>().sprite = screwInsertedImage;
            GetComponent<SpriteRenderer>().color = ColorLerp(startColor, Color.yellow, endColor, 0f);
            progressBarBackgroundImage.enabled = true;
        }

        private Color ColorLerp(Color startColor, Color intermediateColor, Color endColor, float fraction)
        {
            float intermediateFraction;
            if (fraction<=0.5)
            {
               intermediateFraction = fraction + 0.5f;
               return Color.Lerp(startColor, intermediateColor, intermediateFraction);
            }
            else
            {
                intermediateFraction = fraction - 0.5f;
                intermediateFraction *= 2f;
                return Color.Lerp(startColor, endColor, intermediateFraction);
            }
            
        }
    }
}

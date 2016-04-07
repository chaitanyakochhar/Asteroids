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

        private Image progressBar;
        private Image progressBarBackground;
        private float tapsMade = 0;

        public void Start()
        {
            progressBar = transform.GetChild(0).GetChild(0).GetComponent<Image>();
            progressBarBackground = transform.GetChild(0).GetChild(1).GetComponent<Image>();
            progressBarBackground.enabled = false;
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
                        TapToTighten();
                        break;
                    }
            }
        }
        public override void MoveInstrument(Command c, Touch t)
        {
            if (t.fingerId == fingerID && screwState == ScrewState.LOOSE)
            {
                print("FingerID match for " + name);
                location = c.worldPoint;
                location.z = transform.position.z;
                transform.position = location;
            }
        }
        public override void DeactivateInstrument(Command c, Touch t)
        {
            if (screwState == ScrewState.LOOSE)
                base.DeactivateInstrument(c, t);
        }

        #endregion


        public void TapToTighten()
        {
            if (tapsMade < numberOfTapsNeeded)
            {
                tapsMade++;
                float fraction = tapsMade / numberOfTapsNeeded;
                progressBar.fillAmount = fraction;
            }

            if(tapsMade == numberOfTapsNeeded)
            {
                progressBar.fillAmount = 1;
                progressBar.enabled = false;
                progressBarBackground.enabled = false;
                screwState = ScrewState.TIGHT;
            }

        }

        public void ChangeState(Vector3 locationOfSlot)
        {
            if (screwInsertedImage != null)
            {
                screwState = ScrewState.IN_SLOT;
                GetComponent<SpriteRenderer>().sprite = screwInsertedImage;
                transform.position = locationOfSlot;
                tapsMade++;
                float fraction = tapsMade / numberOfTapsNeeded;
                print(fraction);
                progressBar.fillAmount = fraction;
                progressBarBackground.enabled = true;

            }
        }
    }
}

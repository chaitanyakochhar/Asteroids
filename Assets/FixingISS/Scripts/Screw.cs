using UnityEngine;
using System.Collections;
namespace FixingISSGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class Screw : Instrument
    {

        public Sprite screwInsertedImage;

        public GameObject target; //Requires a trigger2d to process image change
        public int numberOfTaps = 2;

        private ScrewState screwState = ScrewState.LOOSE;

        public override void ActivateInstrument(Command c, Touch t)
        {
            if (screwState == ScrewState.LOOSE)
                base.ActivateInstrument(c, t);
        }

        public override void MoveInstrument(Command c, Touch t)
        {
            ;
        }

        public override void DeactivateInstrument(Command c, Touch t)
        {
            if (screwState == ScrewState.LOOSE)
                base.DeactivateInstrument(c, t);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Slot>()!=null && collision.GetComponent<Slot>().hasItem == false)
            {
                collision.GetComponent<Slot>().hasItem = true;
                ChangeState();
            }
        }

        public void TapToTighten()
        {
            numberOfTaps--;
        }

        public void ChangeState()
        {
            if(screwInsertedImage!=null)
            {
                screwState = ScrewState.IN_SLOT;
                GetComponent<SpriteRenderer>().sprite = screwInsertedImage;

            }
        }
    }
}

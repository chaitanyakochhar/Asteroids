using UnityEngine;
using System.Collections;
using System;

namespace FixingISSGame
{
    public class Sticker : Item
    {
        public Boolean onStickySurface = false;

        public override void Activate(Command c, Touch t)
        {
            if(fingerID == -1)
            {
                fingerID = t.fingerId;
            }
        }

        public override void ActivateTheseObjectsOnCompletion()
        {
            foreach(GameObject obj in activateOnCompletion)
            {
                obj.SetActive(true);
            }
        }

        public override void Deactivate(Command c, Touch t)
        {
            if(onStickySurface)
            {
                itemState = ItemState.DONE;
            }
            fingerID = -1;
        }

        public override bool Evaluate()
        {
            throw new NotImplementedException();
        }

        public override void Move(Command c, Touch t)
        {
            if(fingerID == t.fingerId && itemState == ItemState.LOOSE)
            {
                Vector3 location;
                location = c.worldPoint;
                location.z = transform.position.z;
                transform.position = location;
            }
        }

        public override void PlaySound(ItemState itemState)
        {
            ;
        }

        public override void StartEffects()
        {
            ;
        }

        public override void StopEffects()
        {
            ;
        }

        protected override IEnumerator bouncyEnable()
        {
            yield return null;
        }

        // Use this for initialization
        void Start()
        {
            itemState = ItemState.LOOSE;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
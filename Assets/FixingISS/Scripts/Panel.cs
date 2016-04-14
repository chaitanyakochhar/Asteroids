using UnityEngine;
using System.Collections;
using SpaceShooterGame;

namespace FixingISSGame
{
    public class Panel : Item
    {

        public override void Activate(Command c, Touch t)
        {
            if (fingerID == -1)
            {
                fingerID = t.fingerId;
            }
        }
        public override void Move(Command c, Touch t)
        {
            Vector3 location;
            if (fingerID == t.fingerId && itemState == ItemState.LOOSE)
            {
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
        public override void ActivateTheseObjectsOnCompletion()
        {
            foreach (GameObject GO in activateOnCompletion)
            {
                GO.SetActive(true);
            }
        }
        protected override IEnumerator bouncyEnable()
        {
            alert.SetActive(true);
            yield return new WaitForSeconds(4);
            alert.SetActive(false);
        }
        public override bool Evaluate()
        {
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
        public void ChangeState(Vector3 locationOfSlot)
        {
            transform.position = locationOfSlot;
            itemState = ItemState.DONE;
        }
        // Use this for initialization
        void Start()
        {
            itemState = ItemState.LOOSE;
            rotator2d = GetComponent<Rotator2D>();
            mover2d = GetComponent<Mover2D>();
            source = GetComponent<AudioSource>();

            if (transform.childCount> 0)
            {
                alert = transform.GetChild(0).gameObject;
                if (alert != null)
                    alert.SetActive(false);
            }
        }
    }
}
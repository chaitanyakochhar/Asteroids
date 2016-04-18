using UnityEngine;
using System.Collections;
using SpaceShooterGame;

namespace FixingISSGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]

    public abstract class Item : MonoBehaviour
    {
        public AudioClip activationSound;
        public AudioClip[] inProgressSound;
        public AudioClip doneSound;

        public GameObject alert;
        public GameObject[] activateOnCompletion;
        protected ItemState itemState;
        protected int fingerID = -1;
        protected AudioSource source;
        protected Rotator2D rotator2d;
        protected Mover2D mover2d;


        public abstract void Activate(Command c, Touch t);
        public abstract void Move(Command c, Touch t);
        public abstract void Deactivate(Command c, Touch t);
        public abstract bool Evaluate();
        public abstract void StartEffects();
        public abstract void StopEffects();
        public abstract void PlaySound(ItemState itemState);
        public abstract void ActivateTheseObjectsOnCompletion();
        protected abstract IEnumerator bouncyEnable();
    }
}

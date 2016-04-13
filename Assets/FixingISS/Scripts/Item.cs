using UnityEngine;
using System.Collections;
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
        protected ItemState itemState;
        protected int fingerID = -1;
        protected AudioSource source;


        public abstract void Activate(Command c, Touch t);
        public abstract void Move(Command c, Touch t);
        public abstract void Deactivate(Command c, Touch t);
        public abstract bool Evaluate();
        public abstract void StartEffects();
        public abstract void StopEffects();
        public abstract void PlaySound(ItemState itemState);
    }
}

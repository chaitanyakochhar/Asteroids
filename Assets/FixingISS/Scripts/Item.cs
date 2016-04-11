using UnityEngine;
using System.Collections;
namespace FixingISSGame
{
    public abstract class Item : MonoBehaviour
    {
        protected ItemState itemState;
        protected int fingerID = -1;

        public abstract void Activate(Command c, Touch t);
        public abstract void Move(Command c, Touch t);
        public abstract void Deactivate(Command c, Touch t);
        public abstract bool Evaluate();
    }
}

using UnityEngine;
using System.Collections;

namespace FixingISSGame
{
    public class Slot : MonoBehaviour
    {

        public bool hasItem { get; set; }

        public void Start()
        {
            hasItem = false;
        }
    }
}

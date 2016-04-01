using UnityEngine;
using System.Collections;
namespace SpaceShooterGame
{
    public class Rotator2D : MonoBehaviour
    {
        public float speed;
        public GameObject center;

        public void Update()
        {
            if(center!= null)
            transform.RotateAround(center.transform.position, Vector3.forward, Time.deltaTime * speed);
        }

    }
}

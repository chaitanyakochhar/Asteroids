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
            transform.Rotate(Vector3.forward, Time.deltaTime * speed, Space.Self);
        }

    }
}

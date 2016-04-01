using UnityEngine;
using System.Collections;

namespace SpaceshipGame
{
    public class Ship : MonoBehaviour
    {
        public GameObject projectile;
        public float projectileSpeed = 5f;

        private Vector3 refAxis;

        public void FireProjectile(Command c)
        {
            if (c != null)
            {
                Vector3 spawnPoint = offset(transform.position, 3f);
                Vector3 directionOfShot = c.worldPoint - spawnPoint;
                directionOfShot.Normalize();
                GameObject GO = Instantiate(projectile, spawnPoint, Quaternion.identity) as GameObject;
                GO.GetComponent<Rigidbody2D>().velocity = projectileSpeed * directionOfShot;
            }
        }

        public void Start()
        {
            refAxis = transform.position;
            refAxis.x = 1f;
            refAxis.y = 0f;
            refAxis.z = 0f;
            Debug.DrawRay(transform.position, refAxis);
        }

        public void Update()
        {
            isPlayerOutOfBounds();

        }

        public void MovePlayer(Vector3 translation)
        {
            transform.Translate(translation);
        }

        private bool isPlayerOutOfBounds()
        {
            Vector3 testVector = Camera.main.WorldToViewportPoint(transform.position);
            if(testVector.x<0.1f || testVector.x>0.8f || testVector.y>0.8f || testVector.y<0.1f)
            return true;
        }

        private Vector3 offset(Vector3 v, float x = 0, float y = 0, float z = 0)
        {
            v.x += x;
            v.y += y;
            v.z += z;
            return v;
        }
    }
}

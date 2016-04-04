using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{

    public class Ship : MonoBehaviour
    {
        public GameObject projectile;
        public float projectileSpeed = 5f;

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

        public void MovePlayer(Vector3 translation)
        {
            transform.Translate(ClampedTranslation(translation));
        }

        private Vector3 ClampedTranslation(Vector3 translation)
        {
            Vector3 testVector = Camera.main.WorldToViewportPoint(transform.position);
            print("Viewport: " + testVector);
            print("Translation:" + translation);
            if (testVector.x <= 0.2f && translation.x < 0f)
            {
                translation.x = 0f;
            }
            if (testVector.x >= 0.8f && translation.x > 0f)
            {
                translation.x = 0f;
            }
            if (testVector.y <= 0.2f && translation.y < 0f)
            {
                translation.y = 0f;
            }
            if (testVector.y >= 0.8f && translation.y > 0f)
            {
                translation.y = 0f;
            }
            return translation;

        }

        private Vector3 offset(Vector3 v, float x = 0, float y = 0, float z = 0)
        {
            v.x += x;
            v.y += y;
            v.z += z;
            return v;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}

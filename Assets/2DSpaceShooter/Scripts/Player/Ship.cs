using UnityEngine;
using System.Collections;

namespace SpaceshipGame
{
    public class Ship : MonoBehaviour
    {
        public GameObject projectile;


        public void FireProjectile(Command c)
        {
            Vector3 directionOfShot = c.worldPoint - transform.position;
            directionOfShot.Normalize();
            GameObject GO = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            GO.GetComponent<Rigidbody2D>().velocity = directionOfShot;

        }

        public void MovePlayer(Vector3 translation)
        {
            transform.Translate(translation);
        }
    }
}

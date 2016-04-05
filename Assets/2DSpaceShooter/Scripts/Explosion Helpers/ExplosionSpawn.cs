using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    public class ExplosionSpawn : MonoBehaviour
    {
        public GameObject ExplosionToSpawn;
        public int asteroidHP = 2;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.parent!=null && collision.transform.parent.name == "Player")
            {
                collision.transform.parent.GetComponent<Ship>().ReduceHP(1);
            }
            else
            {
                asteroidHP--;
                GameObject GO = Instantiate(ExplosionToSpawn, transform.position, Quaternion.identity) as GameObject;
                GO.GetComponent<ExplosionDestroyer>().DestroyExplosion();
                Destroy(collision.gameObject);
                if (asteroidHP <= 0)
                {
                    Destroy(transform.parent.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    public class ExplosionSpawn : MonoBehaviour
    {
        public GameObject ExplosionToSpawn;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject GO = Instantiate(ExplosionToSpawn, transform.position, Quaternion.identity) as GameObject;
            GO.GetComponent<ExplosionDestroyer>().DestroyExplosion();
            Destroy(collision.gameObject);
            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}

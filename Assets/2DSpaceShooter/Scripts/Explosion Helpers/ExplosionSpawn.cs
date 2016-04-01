using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExplosionSpawn : MonoBehaviour
    {
        public GameObject ExplosionToSpawn;

        public void OnTriggerEnter2D(Collider2D collision)
        {
            GameObject GO = Instantiate(ExplosionToSpawn, transform.position, Quaternion.identity) as GameObject;
            GO.GetComponent<ExplosionDestroyer>().DestroyExplosion();
            Destroy(gameObject);
        }
    }
}

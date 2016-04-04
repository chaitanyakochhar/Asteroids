using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    public class BlueLaser : MonoBehaviour
    {
        public void Update()
        {
            if (Mathf.Abs(Camera.main.WorldToViewportPoint(transform.position).x) > 1.2f || Mathf.Abs(Camera.main.WorldToViewportPoint(transform.position).y) > 1.2f)
            {
                GameObject.Find("Player").GetComponent<Ship>().projectileLimit++;
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != "Projectile")
            {
                GameObject.Find("Player").GetComponent<Ship>().projectileLimit++;
                Destroy(gameObject);
            }
        }
    }
}
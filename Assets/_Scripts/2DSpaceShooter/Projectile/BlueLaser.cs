﻿using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    public class BlueLaser : MonoBehaviour
    {
        public void Update()
        {
            if (Mathf.Abs(Camera.main.WorldToViewportPoint(transform.position).x) > 0.9f || Mathf.Abs(Camera.main.WorldToViewportPoint(transform.position).y) > 0.9f)
            {
                GameObject.Find("Player").GetComponent<Ship>().projectileLimit++;
                Destroy(gameObject);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag != "Projectile")
            {
                if(collision.transform.GetComponent<OnTriggerStartEffect>()!=null)
                {
                    if (collision.transform.GetComponent<OnTriggerStartEffect>().BlockProjectiles)
                    {
                        GameObject.Find("Player").GetComponent<Ship>().projectileLimit++;
                        Destroy(gameObject);
                    }
                }

                else
                {
                    GameObject.Find("Player").GetComponent<Ship>().projectileLimit++;
                    Destroy(gameObject);
                }

            }
        }
    }
}
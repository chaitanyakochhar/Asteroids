﻿using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    public class Mover2D : MonoBehaviour
    {
        public GameObject[] waypoints;
        public float timeBetweenPoints = 1f;
        public float restPeriod = 0f;
        private float startTime;

        void Start()
        {
            StartCoroutine(MoveBetweenPoints());
        }

        private IEnumerator MoveBetweenPoints()
        {
            while (true)
            {
                foreach(GameObject waypoint in waypoints)
                {
                    Vector3 initialPosition = transform.position;
                    startTime = Time.time;
                    while((Time.time - startTime) <= timeBetweenPoints)
                    {
                        transform.position = Vector3.Lerp(initialPosition, waypoint.transform.position, (Time.time - startTime) / timeBetweenPoints);
                        yield return null;
                    }
                    yield return null;
                }
                yield return new WaitForSeconds(restPeriod);
            }
        }

    }
}
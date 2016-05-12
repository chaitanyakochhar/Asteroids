using UnityEngine;
using System.Collections;
using System;

namespace SpaceShooterGame
{
    public class Mover2D : Effect
    {
        public GameObject[] waypoints;
        public float timeBetweenPoints = 1f;
        public float restPeriod = 0f;
        public bool USE_EFFECT_ON_START = false;

        private float startTime;

        void Start()
        {
            if (USE_EFFECT_ON_START)
                StartEffect();
        }

        public void Toggle(bool toggle)
        {
            if (toggle)
            {
                StartCoroutine(MoveBetweenPoints());
            }
            else
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator MoveBetweenPoints()
        {
            while (true)
            {
                foreach (GameObject waypoint in waypoints)
                {
                    Vector3 initialPosition = transform.position;

                    startTime = Time.time;
                    while ((Time.time - startTime) <= timeBetweenPoints)
                    {
                        transform.position = Vector3.Lerp(initialPosition, waypoint.transform.position, (Time.time - startTime) / timeBetweenPoints);
                        yield return null;
                    }
                    yield return null;
                }
                yield return new WaitForSeconds(restPeriod);
            }
        }

        public override void StartEffect()
        {
            Toggle(true);
        }

        public override void StopEffect()
        {
            Toggle(false);
        }

        public override IEnumerator EffectCoroutine()
        {
            yield return null;
        }
    }
}
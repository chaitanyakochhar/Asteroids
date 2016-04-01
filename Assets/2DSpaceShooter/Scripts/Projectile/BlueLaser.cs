using UnityEngine;
using System.Collections;

namespace SpaceshipGame
{
    public class BlueLaser : MonoBehaviour
    {
        public void Update()
        {
            if (Mathf.Abs(Camera.main.WorldToViewportPoint(transform.position).x)>1.2f || Mathf.Abs(Camera.main.WorldToViewportPoint(transform.position).y) > 1.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
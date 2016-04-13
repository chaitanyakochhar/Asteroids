using UnityEngine;
using System.Collections;
[RequireComponent(typeof(TrailRenderer))]
public class Fly2D : MonoBehaviour
{
    public Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        Velocity();
    }

    private void Velocity()
    {
        transform.position += velocity * Time.deltaTime;
    }
}

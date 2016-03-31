using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Rotator : MonoBehaviour
{
    public float speed;
    public GameObject center;

    public void Update()
    {
        if (center != null)
        {
            transform.RotateAround(center.transform.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}

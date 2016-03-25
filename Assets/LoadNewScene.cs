using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        print("Bang");
        if(other.tag == "Player")
        {
            SceneManager.LoadScene("DodgingAsteroids_Dummy");
        }
    }
}

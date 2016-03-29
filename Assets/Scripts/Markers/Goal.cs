using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string nextLevel = "DodgingAsteroids_Dummy";

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}

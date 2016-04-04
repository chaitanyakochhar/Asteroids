using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string nextLevel = GameNames.ExitPage;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.Find("Manager") != null)
                GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().result = true;
            //InterAppCommunicationManager.result = true;
            SceneManager.LoadScene(nextLevel);
        }
    }
}

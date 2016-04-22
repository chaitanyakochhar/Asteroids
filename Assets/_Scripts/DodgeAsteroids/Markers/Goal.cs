using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public string nextLevel = GameNames.ExitPage;
    public AudioClip alertClip;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GameObject.Find("UI").GetComponent<UIUpdater>().alienStart == 3)
        {
            if (GameObject.Find("Manager") != null)
                GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().result = true;
            //InterAppCommunicationManager.result = true;
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            if (alertClip != null)
                Camera.main.GetComponent<AudioSource>().PlayOneShot(alertClip);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("BOOM!");
        if (collision.tag == "Player")
        {
            if (GameObject.Find("Manager") != null)
                GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().result = true;
            //InterAppCommunicationManager.result = true;
            SceneManager.LoadScene(nextLevel);
        }
    }
}

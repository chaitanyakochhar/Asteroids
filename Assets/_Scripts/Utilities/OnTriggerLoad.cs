using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class OnTriggerLoad : MonoBehaviour
{
    public string sceneToLoad;

    // Use this for initialization
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

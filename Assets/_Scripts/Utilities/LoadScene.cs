using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public float delayBeforeLoad = 1f;
    public bool clearPlayerDataManager = false;
    public string sceneToLoad;


    public void OnClickListener()
    {
        GetComponent<Button>().interactable = false;
        StartCoroutine(waitThenLoad());
    }

    private IEnumerator waitThenLoad()
    {
        if(clearPlayerDataManager)
        {
            PlayerDataManager p = FindObjectOfType<PlayerDataManager>();
            if(p!=null)
            {
                p.ClearDictionaries();
            }
        }
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene(sceneToLoad);

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PlayButton;
    public GameObject DialogueOnButton;
    public GameObject DialogueOffButton;
    public GameObject DialogueButtonReference;

    private bool collapsed = false;
    private AudioSource[] sources;

    public void Start()
    {
        ToggleCollapse();
        sources = Object.FindObjectsOfType<AudioSource>();
        if(DialogueButtonReference!=null)
        {
            if(GameObject.Find("Data Manager")!=null)
            {
                PlayerDataManager p = GameObject.Find("Data Manager").GetComponent<PlayerDataManager>();
                if(p.readByYourself)
                {
                    foreach (AudioSource source in sources)
                    {
                        source.mute = true;
                        DialogueButtonReference.GetComponent<Image>().sprite = DialogueOnButton.GetComponent<SpriteRenderer>().sprite;
                    }
                }
            }
        }
    }

    public void Pause(GameObject caller)
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            if (caller != null)
            {
                caller.GetComponent<Image>().sprite = PlayButton.GetComponent<SpriteRenderer>().sprite;
            }
            AudioPause();
        }

        else
        {
            Time.timeScale = 1f;
            caller.GetComponent<Image>().sprite = PauseButton.GetComponent<SpriteRenderer>().sprite;
            AudioUnpause();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.Find("Data Manager"));
        SceneManager.LoadScene(0);
    }
    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleCollapse()
    {
        collapsed = !collapsed;
        transform.GetChild(0).gameObject.SetActive(!collapsed);
    }

    public void AudioPause()
    {
        foreach(AudioSource source in sources)
        {
            source.Pause();
        }
    }
    public void AudioUnpause()
    {
        foreach (AudioSource source in sources)
        {
            source.UnPause();
        }
    }

    public void Mute(GameObject caller)
    {
        GameObject playerDataManager = GameObject.Find("Data Manager");
        if(playerDataManager!=null)
        {
            if(playerDataManager.GetComponent<PlayerDataManager>()!=null)
            {
                playerDataManager.GetComponent<PlayerDataManager>().ToggleReadByYourself(true);
            }
        }
        foreach(AudioSource source in sources)
        {
            source.mute = !source.mute;
            if(source.mute)
            {
                caller.GetComponent<Image>().sprite = DialogueOnButton.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                caller.GetComponent<Image>().sprite = DialogueOffButton.GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PlayButton;

    private bool collapsed = false;
    private AudioSource[] sources;

    public void Start()
    {
        ToggleCollapse();
        sources = Object.FindObjectsOfType<AudioSource>();
    }

    public void Pause(GameObject caller)
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            caller.GetComponent<Image>().sprite = PlayButton.GetComponent<SpriteRenderer>().sprite;
            Mute();
        }

        else
        {
            Time.timeScale = 1f;
            caller.GetComponent<Image>().sprite = PauseButton.GetComponent<SpriteRenderer>().sprite;
            UnMute();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
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

    public void Mute()
    {
        foreach(AudioSource source in sources)
        {
            source.Pause();
        }
    }
    public void UnMute()
    {
        foreach (AudioSource source in sources)
        {
            source.UnPause();
        }
    }

}

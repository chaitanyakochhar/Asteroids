using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject PauseButton;
    public GameObject PlayButton;

    private bool collapsed = false;

    public void Start()
    {
        ToggleCollapse();
    }

    public void Pause(GameObject caller)
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            caller.GetComponent<Image>().sprite = PlayButton.GetComponent<SpriteRenderer>().sprite;
            Camera.main.GetComponent<AudioListener>().enabled = false;
        }

        else
        {
            Time.timeScale = 1f;
            caller.GetComponent<Image>().sprite = PauseButton.GetComponent<SpriteRenderer>().sprite;
            Camera.main.GetComponent<AudioListener>().enabled = true;
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

}

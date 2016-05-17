using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TapPictureToRestartScene : MonoBehaviour
{
    private void MouseListener()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Command c = Command.createCommandWithHitObjectReference(Input.mousePosition, out hit, true);
            if (c != null && hit.transform.GetComponent<TapPictureToRestartScene>() != null)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    private void TouchListener()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.touches[0];
            switch (t.phase)
            {
                case TouchPhase.Began:
                    {
                        RaycastHit hit;
                        Command c = Command.createCommandWithHitObjectReference(Input.mousePosition, out hit, true);
                        if (c != null && hit.transform.GetComponent<TapPictureToRestartScene>() != null)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                        }
                        break;
                    }
            }
        }
    }
}

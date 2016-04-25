using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    public bool finalResult = true;
    public GameObject[] objectsToAuthenticate;
    public string nextSceneToLoad;

    private int blinkCount = 2;
    private float blinkDuration = 1;

    public void ValidateObjects()
    {
        finalResult = true;
        foreach (GameObject GO in objectsToAuthenticate)
        {
            if (GO != null)
            {
                Authenticator a = GO.GetComponent<Authenticator>();
                if (a != null)
                {
                    finalResult &= a.isAuthenticated;
                }
            }
        }

        if (finalResult)
        {
            print("Firing load level call");
            SceneManager.LoadScene(nextSceneToLoad);
        }
        else
        {
            StartCoroutine(Highlight());
        }
    }

    private IEnumerator Highlight()
    {
        Authenticator a;
        foreach (GameObject GO in objectsToAuthenticate)
        {
            a = GO.GetComponent<Authenticator>();
            if (a != null && a.isAuthenticated == false)
            {
                Image i = null;
                Renderer r = null;
                SpriteRenderer s = null;
                if (GO.GetComponent<RectTransform>() != null)
                {
                    i = GO.GetComponent<Image>();
                }
                else
                {
                    r = GO.GetComponent<Renderer>();
                    s = GO.GetComponent<SpriteRenderer>();
                }
                if (i != null)
                {
                    StartCoroutine(MakeItBlink(i));
                }
                if (r != null)
                {
                    StartCoroutine(MakeItBlink(r.material));
                }
                if (s != null)
                {
                    StartCoroutine(MakeItBlink(s));
                }
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator MakeItBlink(Image image)
    {
        Color c = image.color;
        Color startColor = c;
        Color endColor = c;
        endColor.a = 0;

        float startTime;

        for (int i = 0; i < blinkCount; i++)
        {
            startTime = Time.time;
            while ((Time.time - startTime) <= blinkDuration)
            {
                image.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / blinkDuration);
                yield return null;
            }
            yield return null;
        }

        for (int i = 0; i < blinkCount; i++)
        {
            startTime = Time.time;
            while ((Time.time - startTime) <= blinkDuration)
            {
                image.color = Color.Lerp(endColor, startColor, (Time.time - startTime) / blinkDuration);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator MakeItBlink(Material material)
    {
        Color c = material.color;
        Color startColor = c;
        Color endColor = Color.black;


        float startTime = Time.time;

        for (int i = 0; i < blinkCount; i++)
        {
            while ((Time.time - startTime) <= blinkDuration)
            {
                material.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / blinkDuration);
                yield return null;
            }
            yield return null;
        }

        startTime = Time.time;

        for (int i = 0; i < blinkCount; i++)
        {
            while ((Time.time - startTime) <= blinkDuration)
            {
                material.color = Color.Lerp(endColor, startColor, (Time.time - startTime) / blinkDuration);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

    private IEnumerator MakeItBlink(SpriteRenderer sprite)
    {
        Color c = sprite.color;
        Color startColor = c;
        Color endColor = c;
        endColor.a = 0;

        float startTime = Time.time;

        for (int i = 0; i < blinkCount; i++)
        {
            while ((Time.time - startTime) <= blinkDuration)
            {
                sprite.color = Color.Lerp(startColor, endColor, (Time.time - startTime) / blinkDuration);
                yield return null;
            }
            yield return null;
        }

        startTime = Time.time;

        for (int i = 0; i < blinkCount; i++)
        {
            while ((Time.time - startTime) <= blinkDuration)
            {
                sprite.color = Color.Lerp(endColor, startColor, (Time.time - startTime) / blinkDuration);
                yield return null;
            }
            yield return null;
        }
        yield return null;
    }

}

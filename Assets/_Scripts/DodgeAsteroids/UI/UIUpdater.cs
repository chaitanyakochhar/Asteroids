using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIUpdater : MonoBehaviour
{
    public GameObject Goal;
    public GameObject Arrow1;
    public GameObject Arrow2;
    public string gameOverLevel = GameNames.ExitPage;

    private GameObject lifeHolder;
    private GameObject alienHolder;

    private Image[] aliens;
    private Image[] lives;

    private int lifeCount;
    public int alienCount { get; set; }

    private int lifeStart = 0;
    public int alienStart = 0;

    public void Start()
    {
        lifeHolder = GameObject.Find("Lives");
        alienHolder = GameObject.Find("Aliens Collected");

        if (lifeHolder != null)
        {
            lives = getImageComponentsOfObject(lifeHolder);
            lifeCount = lives.Length;
        }

        if (alienHolder != null)
        {
            aliens = getImageComponentsOfObject(alienHolder);
            alienCount = aliens.Length;
        }

        if (Arrow1 != null)
        {
            Arrow1.GetComponent<SpriteRenderer>().enabled = false;
            Arrow1.GetComponent<BoxCollider>().enabled = false;
        }
        if(Arrow2 != null)
        {
            Arrow2.GetComponent<SpriteRenderer>().enabled = false;
            Arrow2.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Toggle(Image i, bool toggle)
    {
        Color c = i.color;
        if (toggle)
        {
            c.a = 1f;
        }
        else
        {
            c.a = 0f;
        }
        i.color = c;
    }

    public void LostLife()
    {
        if (lifeStart < lifeCount)
        {
            Toggle(lives[lifeStart], false);

        }
        lifeStart++;
        if (lifeStart >= lifeCount)
        {
            print("BOOM! from death");
            if (GameObject.Find("Manager") != null)
            {
                GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>().result = false;
                //InterAppCommunicationManager.result = true;

            }
            SceneManager.LoadScene(gameOverLevel);
        }
    }

    public void FoundAlien()
    {
        if (alienStart <= alienCount)
        {
            Toggle(aliens[alienStart], true);
            alienStart++;
        }

        if (alienStart == 1)
        {
            if (Arrow1 != null)
            {
                Arrow1.GetComponent<SpriteRenderer>().enabled = true;
                Arrow1.GetComponent<BoxCollider>().enabled = true;
                Arrow1.GetComponent<Bouncy>().StartBouncy();
            }
        }

        if (alienStart == 2)
        {
            if (Arrow2 != null)
            {
                Arrow2.GetComponent<SpriteRenderer>().enabled = true;
                Arrow2.GetComponent<BoxCollider>().enabled = true;
                Arrow2.GetComponent<Bouncy>().StartBouncy();
            }
        }
        if (alienStart == alienCount)
        {
            Goal.GetComponent<BoxCollider>().enabled = true;
            Goal.GetComponent<Bouncy>().StartBouncy();
        }

    }

    private Image[] getImageComponentsOfObject(GameObject GO)
    {
        List<Image> images = new List<Image>();
        foreach (Transform child in GO.transform)
        {
            images.Add(child.GetComponent<Image>());
        }
        return images.ToArray();
    }

}

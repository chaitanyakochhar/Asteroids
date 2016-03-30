using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIUpdater : MonoBehaviour
{
    public GameObject Goal;
    public GameObject Arrow;

    private GameObject lifeHolder;
    private GameObject alienHolder;

    private Image[] aliens;
    private Image[] lives;

    private int lifeCount;
    private int alienCount;

    private int lifeStart = 0;
    private int alienStart = 0;

    public void Start()
    {
        lifeHolder = GameObject.Find("Lives");
        alienHolder = GameObject.Find("Aliens Collected");

        lives = getImageComponentsOfObject(lifeHolder);
        aliens = getImageComponentsOfObject(alienHolder);

        lifeCount = lives.Length;
        alienCount = aliens.Length;

        Arrow.GetComponent<SpriteRenderer>().enabled = false;
        Arrow.GetComponent<BoxCollider>().enabled = false;
        Goal.GetComponent<BoxCollider>().enabled = false;
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
        if (lifeStart > lifeCount)
        {
            SceneManager.LoadScene("DodgingAsteroids_Dummy");
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
            Arrow.GetComponent<SpriteRenderer>().enabled = true;
            Arrow.GetComponent<BoxCollider>().enabled = true;
            Arrow.GetComponent<Bouncy>().StartBouncy();
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
        foreach(Transform child in GO.transform)
        {
            images.Add(child.GetComponent<Image>());
        }
        return images.ToArray();
    }

}

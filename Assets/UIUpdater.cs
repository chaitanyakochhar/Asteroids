using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        lives = lifeHolder.GetComponentsInChildren<Image>();
        aliens = alienHolder.GetComponentsInChildren<Image>();

        lifeCount = lives.Length;
        alienCount = aliens.Length;

        Arrow.GetComponent<SpriteRenderer>().enabled = false;
        Goal.GetComponent<BoxCollider>().enabled = false;
        Goal.GetComponent<Bouncy>().enabled = false;
    }

    private void Toggle(Image i, bool toggle)
    {
        Color c = i.color;
        if (toggle)
        {
            c.a = 255f;
        }
        else
        {
            c.a = 30f;
        }
        i.color = c;
    }

    public void LostLife()
    {
        if (lifeStart <= lifeCount)
        {
            Toggle(lives[lifeStart], false);
            lifeStart++;
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
        }
        if (alienStart == alienCount)
        {
            Goal.GetComponent<BoxCollider>().enabled = true;
            Goal.GetComponent<Bouncy>().enabled = true;
        }

    }
}

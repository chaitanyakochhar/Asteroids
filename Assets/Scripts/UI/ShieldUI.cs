using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldUI : MonoBehaviour
{
    private Image shieldBar;
    private SpriteRenderer shieldGraphic;
    private Player player;
    public float shieldDuration = 2f;
    public float shieldCooldown = 3f;

    public void Start()
    {
        shieldBar = GameObject.Find("Shield Timer Bar").GetComponent<Image>();
        shieldGraphic = GameObject.Find("Shield").GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    IEnumerator FillUpAndTickDownShieldMeter()
    {
        player.ToggleShield();
        ToggleShieldButton(false);

        float startTime = Time.time;
        shieldGraphic.enabled = true;
        while((Time.time - startTime) <= shieldDuration)
        {
            shieldBar.fillAmount = Mathf.Lerp(1f, -0.1f, (Time.time - startTime) / shieldDuration);
            yield return null;
        }

        shieldGraphic.enabled = false;
        player.ToggleShield();
        AdjustShieldAlpha(0.3f);
        startTime = Time.time;
        while ((Time.time - startTime) <= shieldCooldown)
        {
            GetComponent<Image>().fillAmount = Mathf.Lerp(0f, 1.2f, (Time.time - startTime) / shieldCooldown);
            yield return null;
        }

        ToggleShieldButton(true);
        yield return null;
    }

    public void onShieldButtonPressed()
    {
        StartCoroutine(FillUpAndTickDownShieldMeter());
    }

    private void ToggleShieldButton(bool toggle)
    {
        GameObject GO = GameObject.Find("Shield Button");
        Image i = GO.GetComponent<Image>();
        Button b = GO.GetComponent<Button>();
        Color c = i.color;
        if (toggle)
        {
            b.enabled = true;
            c.a = 1f; 
        }
        else
        {
            b.enabled = false;
            c.a = 0f;
        }
        i.color = c;
    }

    private void AdjustShieldAlpha(float alpha)
    {
        GameObject GO = GameObject.Find("Shield Button");
        Image i = GO.GetComponent<Image>();
        Color c = i.color;
        c.a = alpha;
        i.color = c;
    }
}

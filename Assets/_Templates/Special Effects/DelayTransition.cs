using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class DelayTransition : Effect
{
    public int delaySeconds = 5;
    public string sceneToLoad;
    public override IEnumerator EffectCoroutine()
    {
        yield return new WaitForSeconds(delaySeconds);
        SceneManager.LoadScene(sceneToLoad);
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        StopAllCoroutines();
    }
}

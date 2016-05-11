using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AnimateOnClick : Effect
{
    public bool START_ON_PLAY = false;
    public int animationCount = 1;
    public float animationMultiplier = 1;
    public AudioClip soundToPlay;

    private AnimatorStateInfo stateInfo;
    private float animationLength;
    private Animator animator;
    private bool isPlaying = false;

    public override IEnumerator EffectCoroutine()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            animator.speed = animationMultiplier;
            if (soundToPlay != null)
            {
                GetComponent<AudioSource>().PlayOneShot(soundToPlay);
            }
            print(animationLength);
            yield return new WaitForSeconds(animationLength * animationCount);
            animator.speed = 0;
            isPlaying = false;
        }
        else yield return null;
    }

    public override void StartEffect()
    {
        StartCoroutine(EffectCoroutine());
    }

    public override void StopEffect()
    {
        StopAllCoroutines();
    }

    // Use this for initialization
    void Start()
    {
        if (START_ON_PLAY)
        {
            StartEffect();
        }
        animator = GetComponent<Animator>();
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animationLength = stateInfo.length;
        print(animationLength);
        animator.speed = 0;
    }

    public void OnCollisionEnter(Collision collision)
    {
        print("Boom!");
    }
}

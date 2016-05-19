using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
public class AnimateOnClick : Effect
{
    public bool START_ON_PLAY = false;
    public int animationCount = 1;
    public float animationMultiplier = 1;
    public AudioClip soundToPlay;
    public AudioClip planetDescription;
    public float delayBetween = 0f;

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
                AudioClip[] clips = { soundToPlay, planetDescription };
                StartCoroutine(PlaySoundsInSequenceWithDelay(clips, delayBetween));
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

    private IEnumerator PlaySoundsInSequenceWithDelay(AudioClip[] clips, float interClipDelay)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip != null)
            {
                if(Camera.main.GetComponent<AudioSource>()==null)
                {
                    Camera.main.transform.gameObject.AddComponent<AudioSource>();
                }
                if (Camera.main.GetComponent<AudioSource>().isPlaying)
                {
                    Camera.main.GetComponent<AudioSource>().Stop();
                }
                Camera.main.GetComponent<AudioSource>().PlayOneShot(clip);
                yield return new WaitForSeconds(clip.length + interClipDelay);
                
            }
        }
    }
}

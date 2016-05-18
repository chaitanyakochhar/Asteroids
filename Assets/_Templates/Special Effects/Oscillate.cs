using UnityEngine;
using System.Collections;
using System;

public class Oscillate : Effect
{

    public GameObject center;
    public float degreesToRotate;
    public float speed;
    public bool clockwiseStart = true;
    public bool START_EFFECT_ON_PLAY = true;

    private float degreesRotated = 0;
    

    // Update is called once per frame
    void Update()
    {
        if(START_EFFECT_ON_PLAY)
        {
            StartEffect();
        }
    }

    public override void StartEffect()
    {
        if(!START_EFFECT_ON_PLAY)
        {
            START_EFFECT_ON_PLAY = true;
        }
        if (clockwiseStart)
        {
            transform.RotateAround(center.transform.position, Vector3.forward, Time.deltaTime * speed);
        }
        else
        {
            transform.RotateAround(center.transform.position, Vector3.forward, -(Time.deltaTime * speed));
        }
        degreesRotated += Time.deltaTime * speed;
        if(degreesRotated>=degreesToRotate)
        {
            degreesRotated = 0f;
            clockwiseStart = !clockwiseStart;
        }

    }

    public override void StopEffect()
    {
        START_EFFECT_ON_PLAY = false;
    }

    public override IEnumerator EffectCoroutine()
    {
        yield return null;
    }
}

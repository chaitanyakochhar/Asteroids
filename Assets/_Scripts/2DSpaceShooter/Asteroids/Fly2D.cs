using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(TrailRenderer))]
public class Fly2D : Effect
{
    public Vector3 velocity;
    public bool START_ON_PLAY = false;

    public override IEnumerator EffectCoroutine()
    {
        yield return null;
    }

    public override void StartEffect()
    {
        START_ON_PLAY = true;
    }

    public override void StopEffect()
    {
        START_ON_PLAY = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (START_ON_PLAY)
            Velocity();
    }

    private void Velocity()
    {
        transform.position += velocity * Time.deltaTime;
    }
}

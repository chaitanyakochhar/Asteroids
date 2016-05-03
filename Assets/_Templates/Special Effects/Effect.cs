using UnityEngine;
using System.Collections;

public abstract class Effect : MonoBehaviour {

    public abstract void StartEffect();
    public abstract void StopEffect();
    public abstract IEnumerator EffectCoroutine();
}

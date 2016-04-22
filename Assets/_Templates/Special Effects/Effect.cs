using UnityEngine;
using System.Collections;

public abstract class Effect : MonoBehaviour {

    public abstract void StartEffect();
    public abstract IEnumerator EffectCoroutine();
}

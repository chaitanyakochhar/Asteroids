using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    public class ExplosionDestroyer : MonoBehaviour
    {
        public void DestroyExplosion()
        {
            StartCoroutine(BoomCoroutine());
        }

        private IEnumerator BoomCoroutine()
        {
            yield return new WaitForSeconds(2);
            Destroy(gameObject);
        }
    }
}

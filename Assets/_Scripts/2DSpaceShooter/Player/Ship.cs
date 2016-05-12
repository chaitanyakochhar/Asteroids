using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{

    public class Ship : MonoBehaviour
    {
        public GameObject projectile;
        public float projectileSpeed = 5f;
        public int projectileLimit = 10;
        public int HP = 5;
        public Vector3 offsetVector;

        
        public void FireProjectile(Command c)
        {
            if (c != null && projectileLimit>=0)
            {
                Vector3 spawnPoint = offset(transform.position, offsetVector.x,offsetVector.y,offsetVector.z );
                Vector3 directionOfShot = c.worldPoint - spawnPoint;
                directionOfShot.Normalize();
                GameObject GO = Instantiate(projectile, spawnPoint, Quaternion.identity) as GameObject;
                GO.GetComponent<Rigidbody2D>().velocity = projectileSpeed * directionOfShot;
                projectileLimit--;
            }
        }

        #region MOVEMENT RELATED
        public void MovePlayer(Vector3 translation)
        {
            transform.Translate(ClampedTranslation(translation));
        }

        private Vector3 ClampedTranslation(Vector3 translation)
        {
            Vector3 testVector = Camera.main.WorldToViewportPoint(transform.position);
            if (testVector.x <= 0.2f && translation.x < 0f)
            {
                translation.x = 0f;
            }
            if (testVector.x >= 0.8f && translation.x > 0f)
            {
                translation.x = 0f;
            }
            if (testVector.y <= 0.2f && translation.y < 0f)
            {
                translation.y = 0f;
            }
            if (testVector.y >= 0.8f && translation.y > 0f)
            {
                translation.y = 0f;
            }
            return translation;

        }

        private Vector3 offset(Vector3 v, float x = 0, float y = 0, float z = 0)
        {
            v.x += x;
            v.y += y;
            v.z += z;
            return v;
        }
        #endregion

        public void ReduceHP(int amount)
        {
            HP -= amount;
            if(HP<0)
            {
                //This call is there in the UI, need to fix that, need to put it in the GameManager
                //InterAppCommunicationManager i = GameObject.Find("Manager").GetComponent<InterAppCommunicationManager>();
                //i.LoadSceneFromURL(GameNames.ExitPage);
            }
            else
            {
                GameObject.Find("UI").GetComponent<UIUpdater>().LostLife();
                StartCoroutine(BlinkWhenHit());
            }
        }

        private IEnumerator BlinkWhenHit()
        {
            SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
            PolygonCollider2D collider = transform.GetChild(0).GetComponent<PolygonCollider2D>();
            Color c;
            collider.enabled = false;
            for(int i = 0; i<5; i++)
            {
                c = spriteRenderer.color;
                c.a = 0;
                spriteRenderer.color = c;
                yield return new WaitForSeconds(0.2f);
                c.a = 1;
                spriteRenderer.color = c;
                yield return new WaitForSeconds(0.2f);

            }
            collider.enabled = true;
            yield return null;
        }
    }
}

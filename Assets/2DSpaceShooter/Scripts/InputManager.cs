using UnityEngine;
using System.Collections;

namespace SpaceShooterGame
{
    public class InputManager : MonoBehaviour
    {
        public float xMovementMultiplier = 0.2f;
        public float yMovementMultiplier = 0.2f;
        private Ship player;
        private VirtualJoystick virtualJoystick;


        public void Start()
        {
            if (GameObject.Find("Player") != null)
            {
                player = GameObject.Find("Player").GetComponent<Ship>();

            }
            //virtualJoystick = GameObject.Find("Joystick Background").GetComponent<VirtualJoystick>();
        }

        public void Update()
        {
            if (player != null)
            {
                sampleJoystick();
                sampleKeyboard();
                processTouch();
                processClick();
            }
        }

        //Create command only for touches that are on the screen
        private void processTouch()
        {
            foreach (Touch t in Input.touches)
            {
                switch (t.phase)
                {
                    case TouchPhase.Ended:
                        {
                            player.FireProjectile(Command.createCommandWithoutRaycast(t.position));
                            break;
                        }
                }
            }
        }

        private void processClick()
        {
            if (Input.GetMouseButtonUp(0))
            {
                player.FireProjectile(Command.createCommandWithoutRaycast(Input.mousePosition));
            }
        }

        //Sample input from virtual joystick, will figure out how to disable fo
        private void sampleJoystick()
        {
            if (virtualJoystick != null)
            {
                Vector3 translation = virtualJoystick.SamplePosition();
                translation.x *= xMovementMultiplier;
                translation.y *= yMovementMultiplier;
                player.MovePlayer(translation);
            }

        }

        private void sampleKeyboard()
        {
            Vector3 translation = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                translation.y += 0.2f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                translation.x -= 0.2f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                translation.y -= 0.2f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                translation.x += 0.2f;
            }
            player.MovePlayer(translation);
        }

    }
}

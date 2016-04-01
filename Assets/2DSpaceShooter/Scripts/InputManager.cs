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
            player = GameObject.Find("Player").GetComponent<Ship>();
            virtualJoystick = GameObject.Find("Joystick Background").GetComponent<VirtualJoystick>();
        }

        public void Update()
        {
            sampleJoystick();
            processTouch();
            processClick();
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
            Vector3 translation = virtualJoystick.SamplePosition();
            translation.x *= xMovementMultiplier;
            translation.y *= yMovementMultiplier;
            player.MovePlayer(translation);

        }

    }
}

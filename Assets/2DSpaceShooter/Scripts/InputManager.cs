using UnityEngine;
using System.Collections;

namespace SpaceshipGame
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
                            player.FireProjectile(Command.createCommand(t.position));
                            break;
                        }
                }
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

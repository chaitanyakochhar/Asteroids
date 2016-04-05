using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FixingISSGame
{
    public class InputManager : MonoBehaviour
    {
        public List<Instrument> instruments;

        public void Start()
        {
            GameObject[] GOs = GameObject.FindGameObjectsWithTag("Instrument");
            instruments = new List<Instrument>();
            foreach (GameObject GO in GOs)
            {
                instruments.Add(GO.GetComponent<Instrument>());
            }
            Input.multiTouchEnabled = true;
        }

        public void Update()
        {
            touchListener();
            mouseRaycaster();
        }

        public void touchListener()
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch t in Input.touches)
                {
                    switch (t.phase)
                    {
                        case TouchPhase.Began:
                            {
                                RaycastHit hit;
                                Command c = Command.createCommandWithHitObjectReference(t.position, out hit);
                                if (c != null)
                                {
                                    if (hit.transform.tag == "Instrument")
                                    {
                                        hit.transform.GetComponent<Instrument>().ActivateInstrument(c, t);
                                    }
                                }

                                break;
                            }
                        case TouchPhase.Moved:
                            {
                                foreach (Instrument instrument in instruments)
                                {
                                    instrument.MoveInstrument(Command.createCommandWithoutRaycast(t.position), t);
                                }
                                break;
                            }
                        case TouchPhase.Ended:
                            {
                                foreach (Instrument instrument in instruments)
                                {
                                    instrument.DeactivateInstrument(Command.createCommandWithoutRaycast(t.position), t);
                                }

                                break;
                            }
                    }
                }
            }

        }

        public void mouseRaycaster()
        {
            if (Input.GetMouseButton(0))
            {
                print("Boom");
                RaycastHit hit;
                Command c = Command.createCommandWithHitObjectReference(Input.mousePosition, out hit);
                if (c != null)
                {
                    hit.transform.GetComponent<ColorMesh>().RaycastListener(c, hit);
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FixingISSGame
{
    public class InputManager : MonoBehaviour
    {
        private GameObject[] ItemGOs;
        private List<Item> items;

        private RaycastHit hit_mouse;
        private Command c_mouse;

        #region MonoBehaviors
        public void Start()
        {
            Input.multiTouchEnabled = true;
            ItemGOs = GameObject.FindGameObjectsWithTag("Instrument");
            items = getItems(ItemGOs);

        }
        public void Update()
        {
            touchListener();
            if (Application.isEditor)
            {
                mouseMover();
            }
        }
        #endregion

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
                                Command c = Command.createCommandWithHitObjectReferenceIgnoreUI(t.position, out hit);
                                if (c == null)
                                {
                                    print("Command is null. Dammit.");
                                }
                                if (c != null)
                                {
                                    if (hit.transform.tag == "Instrument")
                                    {
                                        hit.transform.GetComponent<Item>().Activate(c, t);
                                    }
                                }

                                break;
                            }
                        case TouchPhase.Moved:
                            {
                                foreach (Item item in items)
                                {
                                    item.Move(Command.createCommandWithoutRaycast(t.position), t);
                                }
                                break;
                            }
                        case TouchPhase.Ended:
                            {
                                foreach (Item item in items)
                                {
                                    item.Deactivate(Command.createCommandWithoutRaycast(t.position), t);
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
                RaycastHit hit;
                Command c = Command.createCommandWithHitObjectReference(Input.mousePosition, out hit);
                if (c != null && hit.transform.tag == "Paintable")
                {
                    hit.transform.GetComponent<ColorMesh>().RaycastListener(hit.textureCoord, Color.black);
                }
            }
        }
        public void mouseMover()
        {


            if(Input.GetMouseButtonDown(0))
            {
                c_mouse = Command.createCommandWithHitObjectReferenceIgnoreUI(Input.mousePosition, out hit_mouse);
                if(c_mouse!=null)
                {
                    if(hit_mouse.transform.tag == "Instrument")
                    {
                        Touch t = new Touch();
                        hit_mouse.transform.GetComponent<Item>().Activate(c_mouse, t);
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                c_mouse = Command.createCommandWithHitObjectReference(Input.mousePosition, out hit_mouse);
                if(c_mouse!=null)
                {
                    if (hit_mouse.transform.tag == "Instrument")
                    {
                        Touch t = new Touch();
                        hit_mouse.transform.GetComponent<Item>().Move(c_mouse, t);
                    }
                }
            }

        }
        public List<Item> getItems(GameObject[] GOs)
        {
            List<Item> i = new List<Item>();
            foreach (GameObject GO in GOs)
            {
                if (GO.GetComponent<Item>() == null)
                {
                    print("NULLLLLLL");
                }
                i.Add(GO.GetComponent<Item>());
            }
            return i;
        }
    }
}

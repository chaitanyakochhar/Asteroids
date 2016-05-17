using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FixingISSGame
{
    public class InputManager : MonoBehaviour
    {
        private GameObject[] ItemGOs;
        private List<Item> items;

        private RaycastHit hit_touch;
        private Command c_touch;

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
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXWebPlayer:
                case RuntimePlatform.WebGLPlayer:
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.LinuxPlayer:
                    {
                        mouseMover();
                        break;
                    }
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    {
                        touchListener();
                        break;
                    }

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
                                Command c;
                                c = Command.createCommandWithHitObjectReferenceIgnoreUI(t.position, out hit);
                                if (c != null)
                                {
                                    if (hit.transform.GetComponent<Item>() != null)
                                        hit.transform.GetComponent<Item>().Activate(c, t);
                                }
                                break;
                            }
                        case TouchPhase.Moved:
                            {
                                //RaycastHit hit;
                                Command c = Command.createCommandWithoutRaycast(t.position);
                                if (c != null)
                                {
                                    foreach (Item item in items)
                                    {
                                        item.Move(c, t);
                                    }
                                }
                                break;
                            }
                        case TouchPhase.Ended:
                            {
                                //RaycastHit hit;
                                Command c = Command.createCommandWithoutRaycast(t.position);
                                if (c != null)
                                {
                                    foreach (Item item in items)
                                    {
                                        item.Deactivate(c, t);
                                    }
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


            if (Input.GetMouseButtonDown(0))
            {
                c_mouse = Command.createCommandWithHitObjectReferenceIgnoreUI(Input.mousePosition, out hit_mouse);
                if (c_mouse != null)
                {
                    if (hit_mouse.transform.tag == "Instrument")
                    {
                        Touch t = new Touch();
                        hit_mouse.transform.GetComponent<Item>().Activate(c_mouse, t);
                    }
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (c_mouse != null)
                {
                    if (hit_mouse.transform.tag == "Instrument")
                    {
                        Touch t = new Touch();
                        hit_mouse.transform.GetComponent<Item>().Move(Command.createCommandWithoutRaycast(Input.mousePosition), t);
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                c_mouse = Command.createCommandWithHitObjectReferenceIgnoreUI(Input.mousePosition, out hit_mouse);
                if (c_mouse != null)
                {
                    Touch t = new Touch();
                    hit_mouse.transform.GetComponent<Item>().Deactivate(c_mouse, t);
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

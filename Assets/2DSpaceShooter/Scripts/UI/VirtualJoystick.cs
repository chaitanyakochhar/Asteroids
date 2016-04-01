using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace SpaceShooterGame
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        private Image backgroundImage;
        private Image joystickImage;
        private Vector3 inputVector;

        // Use this for initialization
        void Start()
        {
            inputVector = new Vector3();
            backgroundImage = GetComponent<Image>();
            joystickImage = transform.GetChild(0).GetComponent<Image>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 pos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
            {
                pos.x = pos.x / backgroundImage.rectTransform.sizeDelta.x;
                pos.y = pos.y / backgroundImage.rectTransform.sizeDelta.y;
                inputVector.x = pos.x;
                inputVector.y = pos.y;
                inputVector.z = 0f;
                if (inputVector.magnitude >= 1.0f)
                {
                    inputVector = inputVector.normalized;
                }

                joystickImage.rectTransform.anchoredPosition = new Vector2(inputVector.x * backgroundImage.rectTransform.sizeDelta.x / 3, inputVector.y * backgroundImage.rectTransform.sizeDelta.y / 3);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inputVector = Vector3.zero;
            joystickImage.rectTransform.anchoredPosition = Vector2.zero;
        }

        public Vector3 SamplePosition()
        {
            return inputVector;
        }
    }
}

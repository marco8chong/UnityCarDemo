using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace CarDemo
{
    public class VirtualGamePadButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent OnPointerDown;
        public UnityEvent OnPointerUp;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            try
            {
                OnPointerDown.Invoke();
            }
            catch
            {
                Debug.LogError("Error invoking button event.");
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            try
            {
                OnPointerUp.Invoke();
            }
            catch
            {
                Debug.LogError("Error invoking button event.");
            }
        }

        public void SetThrottle(bool forward)
        {
            GameDirector.Instance.GameInputManager.SetThrottle(forward);
        }

        public void SetBrakeReverse(bool reverse)
        {
            GameDirector.Instance.GameInputManager.SetBrakeReverse(reverse);
        }

        public void SetLeft(bool left)
        {
            GameDirector.Instance.GameInputManager.SetLeft(left);
        }

        public void SetRight(bool right)
        {
            GameDirector.Instance.GameInputManager.SetRight(right);
        }

        public void SetHandbrake(bool handbrake)
        {
            GameDirector.Instance.GameInputManager.SetHandbrake(handbrake);
        }

        public void SetAddon(bool addOn)
        {
            GameDirector.Instance.GameInputManager.SetAddon(addOn);
        }
    }
}

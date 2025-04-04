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

        public void SetForward(bool forward)
        {
            GameDirector.Instance.GameInputManager.SetForward(forward);
        }

        public void SetReverse(bool reverse)
        {
            GameDirector.Instance.GameInputManager.SetReverse(reverse);
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
    }
}

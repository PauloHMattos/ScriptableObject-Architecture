using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_RAISER_SUBMENU + "On Button Raiser")]
    public class OnButtonEventRaiser : BaseGameEventRaiser
    {
        public string buttonName;
        public EventType eventType;


        private void Update()
        {
            if (string.IsNullOrEmpty(buttonName))
                return;

            if (eventType.HasFlag(EventType.Down) && Input.GetButtonDown(buttonName))
            {
                response.Invoke();
            }
            if (eventType.HasFlag(EventType.Hold) && Input.GetButton(buttonName))
            {
                response.Invoke();
            }
            if (eventType.HasFlag(EventType.Up) && Input.GetButtonUp(buttonName))
            {
                response.Invoke();
            }
        }

        [Flags]
        public enum EventType
        {
            Down = 1,
            Hold = 2,
            Up = 4
        }
    }
}
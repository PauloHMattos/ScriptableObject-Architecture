using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_RAISER_SUBMENU + "On Button Raiser")]
    public class OnButtonEventRaiser : BaseGameEventRaiser
    {
        [Group("General")]
        public string buttonName;
        public EventType eventType;

        protected override void Update()
        {
            if (string.IsNullOrEmpty(buttonName))
                return;

            if (eventType.HasFlag(EventType.Down) && Input.GetButtonDown(buttonName))
            {
                _events.Invoke();
            }
            if (eventType.HasFlag(EventType.Hold) && Input.GetButton(buttonName))
            {
                _events.Invoke();
            }
            if (eventType.HasFlag(EventType.Up) && Input.GetButtonUp(buttonName))
            {
                _events.Invoke();
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
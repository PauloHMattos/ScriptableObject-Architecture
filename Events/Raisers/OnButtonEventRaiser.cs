using System;
using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_RAISER_SUBMENU + "On Button Raiser")]
    public class OnButtonEventRaiser : BaseGameEventRaiser
    {
        [Group("General", "GameManager Icon")]
        public string ButtonName;
        public EventTypeOptions EventType;

        protected override void Update()
        {
            if (string.IsNullOrEmpty(ButtonName))
                return;

            if (EventType.HasFlag(EventTypeOptions.Down) && Input.GetButtonDown(ButtonName))
            {
                _events.Invoke();
            }
            if (EventType.HasFlag(EventTypeOptions.Hold) && Input.GetButton(ButtonName))
            {
                _events.Invoke();
            }
            if (EventType.HasFlag(EventTypeOptions.Up) && Input.GetButtonUp(ButtonName))
            {
                _events.Invoke();
            }
        }

        [Flags]
        public enum EventTypeOptions
        {
            Down = 1,
            Hold = 2,
            Up = 4
        }
    }
}
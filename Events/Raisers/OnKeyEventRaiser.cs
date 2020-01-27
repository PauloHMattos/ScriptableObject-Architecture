using System;
using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.References;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_RAISER_SUBMENU + "On Key Raiser")]
    public class OnKeyEventRaiser : BaseGameEventRaiser
    {
        [Group("General", "GameManager Icon")]
        public KeyCodeReference Key;
        public EventType EventType;

        protected override void Update()
        {
            if (!Key.IsValueDefined)
                return;

            if (EventType.HasFlag(EventTypeOptions.Down) && Input.GetKeyDown(Key.Value))
            {
                _events.Invoke();
            }
            if (EventType.HasFlag(EventTypeOptions.Hold) && Input.GetKey(Key.Value))
            {
                _events.Invoke();
            }
            if (EventType.HasFlag(EventTypeOptions.Up) && Input.GetKeyUp(Key.Value))
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
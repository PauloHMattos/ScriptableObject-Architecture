using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_RAISER_SUBMENU + "On Key Raiser")]
    public class OnKeyEventRaiser : BaseGameEventRaiser
    {
        [Group("General")]
        public KeyCodeReference key;
        public EventType eventType;


        protected override void Update()
        {
            if (!key.IsValueDefined)
                return;

            if (eventType.HasFlag(EventType.Down) && Input.GetKeyDown(key.Value))
            {
                _events.Invoke();
            }
            if (eventType.HasFlag(EventType.Hold) && Input.GetKey(key.Value))
            {
                _events.Invoke();
            }
            if (eventType.HasFlag(EventType.Up) && Input.GetKeyUp(key.Value))
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
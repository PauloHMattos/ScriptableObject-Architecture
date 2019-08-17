using System;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class OnKeyEventRaiser : BaseGameEventRaiser
    {
        public KeyCodeReference key;

        public EventType eventType;


        private void Update()
        {
            if (!key.IsValueDefined)
                return;

            if (Input.GetKeyDown(key.Value))
            {
                response.Invoke();
            }
            if (Input.GetKey(key.Value))
            {
                response.Invoke();
            }
            if (Input.GetKeyUp(key.Value))
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
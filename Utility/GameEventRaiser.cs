using ScriptableObjectArchitecture.Events.Game_Events;
using UnityEngine;

namespace ScriptableObjectArchitecture.Utility
{
    public class GameEventRaiser : MonoBehaviour
    {
        public GameEvent Event;

        public void Raise()
        {
            Event.Raise();
        }
    }
}

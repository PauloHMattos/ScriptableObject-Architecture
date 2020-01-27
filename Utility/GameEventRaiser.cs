using ScriptableObjectArchitecture.Events.GameEvents;
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

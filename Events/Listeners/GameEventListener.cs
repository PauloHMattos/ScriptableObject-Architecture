using ScriptableObjectArchitecture.Events.GameEvents;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_LISTENER_SUBMENU + "Game Event Listener")]
    public class GameEventListener : BaseGameEventListener<GameEventBase, UnityEvent>
    {
    }
}
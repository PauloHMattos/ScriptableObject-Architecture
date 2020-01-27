using ScriptableObjectArchitecture.Events.GameEvents;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_LISTENER_SUBMENU + "int Event Listener")]
    public sealed class IntGameEventListener : BaseGameEventListener<int, IntGameEvent, IntUnityEvent>
    {
    }
}
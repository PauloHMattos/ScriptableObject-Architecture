using ScriptableObjectArchitecture.Events.GameEvents;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_LISTENER_SUBMENU + "Vector3 Event Listener")]
    public sealed class Vector3GameEventListener : BaseGameEventListener<Vector3, Vector3GameEvent, Vector3UnityEvent>
    {
    }
}
using ScriptableObjectArchitecture.Events.GameEvents;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_LISTENER_SUBMENU + "long Event Listener")]
    public sealed class LongGameEventListener : BaseGameEventListener<long, LongGameEvent, LongUnityEvent>
    {
    }
}
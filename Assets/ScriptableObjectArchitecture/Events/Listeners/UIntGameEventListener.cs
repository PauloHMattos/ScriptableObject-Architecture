using ScriptableObjectArchitecture.Events.Game_Events;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_LISTENER_SUBMENU + "uint Event Listener")]
    public sealed class UIntGameEventListener : BaseGameEventListener<uint, UIntGameEvent, UIntUnityEvent>
    {
    }
}
using ScriptableObjectArchitecture.Events.Game_Events;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_LISTENER_SUBMENU + "byte Event Listener")]
    public sealed class ByteGameEventListener : BaseGameEventListener<byte, ByteGameEvent, ByteUnityEvent>
    {
    }
}
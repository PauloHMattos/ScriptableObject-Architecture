using ScriptableObjectArchitecture.Events.Game_Events;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Vector4 Event Listener")]
    public sealed class Vector4GameEventListener : BaseGameEventListener<Vector4, Vector4GameEvent, Vector4UnityEvent>
    {
    }
}
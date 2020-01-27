using ScriptableObjectArchitecture.Events.Game_Events;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Vector2 Event Listener")]
    public sealed class Vector2GameEventListener : BaseGameEventListener<Vector2, Vector2GameEvent, Vector2UnityEvent>
    {
    }
}
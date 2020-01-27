using ScriptableObjectArchitecture.Events.Game_Events;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Listeners
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "short Event Listener")]
    public sealed class ShortGameEventListener : BaseGameEventListener<short, ShortGameEvent, ShortUnityEvent>
    {
    }
}
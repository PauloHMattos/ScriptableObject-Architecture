using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Game Event Listener")]
    [ExecuteInEditMode]
    public class GameEventListener : BaseGameEventListener<GameEventBase, UnityEvent>
    {
    }
}
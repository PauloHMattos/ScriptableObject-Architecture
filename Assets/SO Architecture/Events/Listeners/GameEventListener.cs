using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "Game Event Listener")]
    public class GameEventListener : BaseGameEventListener<GameEventBase, UnityEvent>
    {
        [Group("A"), Label("GameObject", "Prefab Icon"), Required]
        public GameObject obj;
        [Group("A"), Label("Tag"), HelpBox("Warning", HelpBoxType.Warning), Tag(order =1), Color(255, 0, 0)]
        public string _tadfg;
    }
}
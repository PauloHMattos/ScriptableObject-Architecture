using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "ObjectGameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "Object",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 1)]
    public class ObjectGameEvent : GameEventBase<Object>
    {
    } 
}
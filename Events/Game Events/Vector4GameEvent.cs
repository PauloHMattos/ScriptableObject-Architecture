using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "Vector4GameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "Structs/Vector4",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 12)]
    public sealed class Vector4GameEvent : GameEventBase<Vector4>
    {
    } 
}
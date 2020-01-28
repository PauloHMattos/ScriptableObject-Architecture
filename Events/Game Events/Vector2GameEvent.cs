using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "Vector2GameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "Structs/Vector2",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 10)]
    public sealed class Vector2GameEvent : GameEventBase<Vector2>
    {
    } 
}
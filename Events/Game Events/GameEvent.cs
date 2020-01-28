using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [CreateAssetMenu(
        fileName = "GameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "Game Event",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS - 1)]
    public sealed class GameEvent : GameEventBase
    {
    } 
}
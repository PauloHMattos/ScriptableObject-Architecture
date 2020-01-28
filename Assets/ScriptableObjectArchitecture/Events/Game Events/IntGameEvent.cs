using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "IntGameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "int",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 4)]
    public sealed class IntGameEvent : GameEventBase<int>
    {
    } 
}
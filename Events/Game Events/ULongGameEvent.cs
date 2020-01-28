using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "UnsignedLongGameEvent.asset",
        menuName = SoArchitectureUtility.ADVANCED_GAME_EVENT + "ulong",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 17)]
    public sealed class ULongGameEvent : GameEventBase<ulong>
    {
    } 
}
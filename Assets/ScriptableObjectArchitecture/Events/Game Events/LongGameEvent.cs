using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "LongGameEvent.asset",
        menuName = SoArchitectureUtility.ADVANCED_GAME_EVENT + "long",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 9)]
    public sealed class LongGameEvent : GameEventBase<long>
    {
    } 
}
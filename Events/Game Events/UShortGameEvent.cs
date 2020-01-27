using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "UnsignedShortGameEvent.asset",
        menuName = SoArchitectureUtility.ADVANCED_GAME_EVENT + "ushort",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 18)]
    public sealed class UShortGameEvent : GameEventBase<ushort>
    {
    } 
}
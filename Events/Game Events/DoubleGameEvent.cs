using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "DoubleGameEvent.asset",
        menuName = SoArchitectureUtility.ADVANCED_GAME_EVENT + "double",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 8)]
    public sealed class DoubleGameEvent : GameEventBase<double>
    {
    } 
}
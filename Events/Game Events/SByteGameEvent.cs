using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "SignedByteGameEvent.asset",
        menuName = SoArchitectureUtility.ADVANCED_GAME_EVENT + "sbyte",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 15)]
    public sealed class SByteGameEvent : GameEventBase<sbyte>
    {
    } 
}
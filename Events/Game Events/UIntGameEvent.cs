using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "UnsignedIntGameEvent.asset",
        menuName = SoArchitectureUtility.ADVANCED_GAME_EVENT + "uint",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 16)]
    public sealed class UIntGameEvent : GameEventBase<uint>
    {
    } 
}
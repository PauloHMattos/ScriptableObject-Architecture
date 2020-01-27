using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "StringGameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "string",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 2)]
    public sealed class StringGameEvent : GameEventBase<string>
    {
    } 
}
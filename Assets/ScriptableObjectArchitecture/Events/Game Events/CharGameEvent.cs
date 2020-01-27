using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "CharGameEvent.asset",
        menuName = SoArchitectureUtility.ADVANCED_GAME_EVENT + "char",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 7)]
    public sealed class CharGameEvent : GameEventBase<char>
    {
    } 
}
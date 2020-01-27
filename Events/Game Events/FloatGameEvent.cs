using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "FloatGameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "float",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 3)]
    public sealed class FloatGameEvent : GameEventBase<float>
    {
    } 
}
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "Vector3GameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "Structs/Vector3",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 11)]
    public sealed class Vector3GameEvent : GameEventBase<Vector3>
    {
    } 
}
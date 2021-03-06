using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "QuaternionGameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "Structs/Quaternion",
        order = SoArchitectureUtility.ASSET_MENU_ORDER_EVENTS + 13)]
    public sealed class QuaternionGameEvent : GameEventBase<Quaternion>
    {
    } 
}
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "QuaternionGameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENT + "Structs/Quaternion",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 13)]
    public sealed class QuaternionGameEvent : GameEventBase<Quaternion>
    {
    } 
}
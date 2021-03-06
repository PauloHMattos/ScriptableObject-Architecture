using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "GameObjectGameEvent.asset",
        menuName = SoArchitectureUtility.GAME_EVENT + "GameObject",
        order = 120)]
    public sealed class GameObjectGameEvent : GameEventBase<GameObject>
    {
    } 
}
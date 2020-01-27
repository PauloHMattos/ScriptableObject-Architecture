using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.GameEvents
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
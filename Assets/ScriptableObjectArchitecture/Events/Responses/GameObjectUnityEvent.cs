using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture.Events.Responses
{
    [System.Serializable]
    public sealed class GameObjectUnityEvent : UnityEvent<GameObject>
    {
    } 
}
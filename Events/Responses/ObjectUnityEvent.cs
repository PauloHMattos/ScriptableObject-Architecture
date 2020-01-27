using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture.Events.Responses
{
    [System.Serializable]
    public class ObjectUnityEvent : UnityEvent<Object>
    {
    } 
}
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseGameEventRaiser : MonoBehaviour
    {
        public UnityEvent response;
    }
}
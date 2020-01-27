using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    public abstract class BaseGameEventRaiser : SOArchitectureBaseMonoBehaviour
    {
        [FormerlySerializedAs("response"), FormerlySerializedAs("_response")]
        [SerializeField]
        [Group("Response")]
        protected UnityEvent _events;

        protected virtual void Update()
        {
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
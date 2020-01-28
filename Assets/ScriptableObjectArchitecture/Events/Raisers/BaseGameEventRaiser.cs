using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    public abstract class BaseGameEventRaiser : SOArchitectureBaseMonoBehaviour
    {
        [SerializeField]
        [Group("Response", "d_CollabMoved Icon")]
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
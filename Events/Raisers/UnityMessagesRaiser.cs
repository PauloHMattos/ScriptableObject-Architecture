using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_RAISER_SUBMENU + "Unity Messages Raiser")]
    public class UnityMessagesRaiser : SOArchitectureBaseMonoBehaviour
    {
        [Group("On Start")]
        public UnityEvent OnStart;
        [Group("On Update")]
        public UnityEvent OnUpdate;
        [Group("On Late Update")]
        public UnityEvent OnLateUpdate;
        [Group("On Fixed Update")]
        public UnityEvent OnFixedUpdate;

        protected void Start()
        {
            OnUpdate.Invoke();
        }

        protected void Update()
        {
            OnUpdate.Invoke();
        }

        protected virtual void LateUpdate()
        {
            OnLateUpdate.Invoke();
        }

        protected virtual void FixedUpdate()
        {
            OnFixedUpdate.Invoke();
        }
    }
}
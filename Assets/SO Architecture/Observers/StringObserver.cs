using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.OBSERVER_SUBMENU + "String Observer")]
	public sealed class StringObserver : BaseObserver<string, Subject>
    {
        protected override UnityEventBase Response => _response;
        [SerializeField]
        private StringUnityEvent _response = default(StringUnityEvent);

        public override void OnVariableChanged()
        {
            RaiseResponse(_variable.ToString());
        }

        protected override void RaiseResponse(string value)
        {
            _response.Invoke(value);
        }
    }
}
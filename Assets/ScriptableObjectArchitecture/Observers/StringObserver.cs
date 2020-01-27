using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture.Observers
{
	[AddComponentMenu(SOArchitecture_Utility.OBSERVER_SUBMENU + "String Observer")]
	public sealed class StringObserver : BaseObserver<string, Subject>
    {
        protected override UnityEventBase Response => _response;
        public string Format { get => _format; set => _format = value; }

        [Group("Response"), SerializeField]
        private string _format = "";
        [SerializeField]
        private StringUnityEvent _response = default(StringUnityEvent);

        public override void OnVariableChanged()
        {
            RaiseResponse(string.Format(_format, _variable.ToString()));
        }

        protected override void RaiseResponse(string value)
        {
            _response.Invoke(value);
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.OBSERVER_SUBMENU + "String Observer")]
	public sealed class StringObserver : BaseObserver<string, Subject>
    {
        protected override UnityEventBase Response => _response;
        public string Format { get => _format; set => _format = value; }

        [SerializeField]
        private StringUnityEvent _response = default(StringUnityEvent);
        [SerializeField]
        private string _format = "";

        public override void OnVariableChanged()
        {
            RaiseResponse(string.Format(_format, _variable.BaseValue));
        }

        protected override void RaiseResponse(string value)
        {
            _response.Invoke(value);
        }
    }
}
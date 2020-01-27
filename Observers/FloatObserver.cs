using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.References;
using ScriptableObjectArchitecture.Utility;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.Observers
{
	[AddComponentMenu(SoArchitectureUtility.OBSERVER_SUBMENU + "Float Observer")]
	public sealed class FloatObserver : NumericObserver<float, FloatVariable, FloatReference, FloatUnityEvent>
	{
        protected override void RaiseResponse(float value)
        {
            if (_sample)
            {
                value = _modifierCurve.Evaluate(value);
            }
            else
            {
                value = _modifierCurve.Evaluate(value) * value;
            }
            base.RaiseResponse(value);
        }
    }
}
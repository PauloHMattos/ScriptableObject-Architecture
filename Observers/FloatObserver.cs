using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.OBSERVER_SUBMENU + "Float Observer")]
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
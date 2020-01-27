using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.References;
using ScriptableObjectArchitecture.Utility;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.Observers
{

    [AddComponentMenu(SOArchitecture_Utility.OBSERVER_SUBMENU + "Int Observer")]
	public sealed class IntObserver : NumericObserver<int, IntVariable, IntReference, IntUnityEvent>
	{
        protected override void RaiseResponse(int value)
        {
            float val;
            if (_sample)
            {
                val = _modifierCurve.Evaluate(value);
            }
            else
            {
                val = _modifierCurve.Evaluate(value) * value;
            }
            base.RaiseResponse(Mathf.RoundToInt(val));
        }
    }
}
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.Observers
{
    public class BoolObserver : BaseObserver<bool, BoolVariable, BoolUnityEvent>
    {
        public bool invert;

        protected override void RaiseResponse(bool value)
        {
            if (invert)
            {
                value = !value;
            }
            base.RaiseResponse(value);
        }
    }
}
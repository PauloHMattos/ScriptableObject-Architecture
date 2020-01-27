using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.Observers
{
    public class BoolObserver : BaseObserver<bool, BoolVariable, BoolUnityEvent>
    {
        [Group("General", "GameManager Icon")]
        public bool Invert;

        protected override void RaiseResponse(bool value)
        {
            if (Invert)
            {
                value = !value;
            }
            base.RaiseResponse(value);
        }
    }
}
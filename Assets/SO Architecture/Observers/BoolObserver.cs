namespace ScriptableObjectArchitecture
{
    public class BoolObserver : BaseObserver<bool, BoolVariable, BoolUnityEvent>
    {
        [DisplayField("Invert response")]
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
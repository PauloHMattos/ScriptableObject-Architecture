using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.OBSERVER_SUBMENU + "Vector2 Observer")]
    public sealed class Vector2Observer : BaseObserver<Vector2, Vector2Variable, Vector2UnityEvent>
    {
        void Start()
        {
            if (_variable != null)
                RaiseResponse(_variable.Value);
        }
    }
}
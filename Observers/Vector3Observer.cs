using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.OBSERVER_SUBMENU + "Vector3 Observer")]
    public sealed class Vector3Observer : BaseObserver<Vector3, Vector3Variable, Vector3UnityEvent>
    {
    }
}
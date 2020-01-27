using ScriptableObjectArchitecture.Events.Responses;
using ScriptableObjectArchitecture.Utility;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.Observers
{
    [AddComponentMenu(SoArchitectureUtility.OBSERVER_SUBMENU + "Vector3 Observer")]
    public sealed class Vector3Observer : BaseObserver<Vector3, Vector3Variable, Vector3UnityEvent>
    {
    }
}
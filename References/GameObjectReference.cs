using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class GameObjectReference : BaseReference<GameObject, GameObjectVariable>
    {
        public GameObjectReference() : base() { }
        public GameObjectReference(GameObject value) : base(value) { }
    } 
}
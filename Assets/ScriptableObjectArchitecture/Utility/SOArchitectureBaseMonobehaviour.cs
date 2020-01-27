using UnityEngine;

namespace ScriptableObjectArchitecture.Utility
{
    /// <summary>
    /// Base class for SOArchitecture assets
    /// Implements developer descriptions
    /// </summary>
    public abstract class SoArchitectureBaseMonoBehaviour : MonoBehaviour
    {
#if UNITY_EDITOR
#pragma warning disable 0414
        [SerializeField]
        private int _showGroups = 0;
        [SerializeField]
        private bool _showButttons = false;
        [SerializeField]
        private DeveloperDescription _developerDescription = new DeveloperDescription();
#pragma warning restore
#endif
    } 
}
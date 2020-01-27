using ScriptableObjectArchitecture.Variables;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public sealed class SceneReference : BaseReference<SceneInfo, SceneVariable>
    {
        public SceneReference()
        {
        }
        public SceneReference(SceneInfo value) : base(value)
        {
        }
    }
}
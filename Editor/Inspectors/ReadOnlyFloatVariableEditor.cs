using ScriptableObjectArchitecture.Variables;
using UnityEditor;

namespace Assets.ScriptableObjectArchitecture.Editor.Inspectors
{

    [CustomEditor(typeof(ReadOnlyFloatVariable), true)]
    public class ReadOnlyFloatVariableEditor : BaseVariableEditor
    {
        protected override void DrawReadonlyField()
        {
        }
    }
}
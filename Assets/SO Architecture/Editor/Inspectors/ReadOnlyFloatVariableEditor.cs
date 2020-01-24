using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{

    [CustomEditor(typeof(ReadOnlyFloatVariable), true)]
    public class ReadOnlyFloatVariableEditor : BaseVariableEditor
    {
        protected override void DrawReadonlyField()
        {
        }
    }
}
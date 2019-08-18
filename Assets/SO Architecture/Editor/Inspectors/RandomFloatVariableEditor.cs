using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(RandomFloatVariable), true)]
    public class RandomFloatVariableEditor : ReadOnlyVariableEditor
    {
        private RandomFloatVariable Target { get { return (RandomFloatVariable)target; } }
        
        protected override void DrawValue()
        {
            using (var scope = new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.FloatField("Value", Target.Value);
            }

            var newSeed = EditorGUILayout.IntField("Seed", Target.Seed);
            if (newSeed != Target.Seed)
            {
                Target.Seed = newSeed;
            }
        }
    }
}
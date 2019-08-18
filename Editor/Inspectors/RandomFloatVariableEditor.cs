using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(RandomFloatVariable), true)]
    public class RandomFloatVariableEditor : ReadOnlyFloatVariableEditor
    {
        private RandomFloatVariable Target { get { return (RandomFloatVariable)target; } }

        protected override void DrawValue()
        {
            // Call Value.get so the displayed value also gets updated
            var value = Target.Value;
            base.DrawValue();


            var newSeed = EditorGUILayout.IntField("Seed", Target.Seed);
            if (newSeed != Target.Seed)
            {
                Target.Seed = newSeed;
            }
        }
    }
}
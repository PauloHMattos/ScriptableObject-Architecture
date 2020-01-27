using ScriptableObjectArchitecture.Variables;
using UnityEditor;

namespace Assets.ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(RandomFloatVariable), true)]
    public class RandomFloatVariableEditor : ReadOnlyFloatVariableEditor
    {
        private SerializedProperty _useTimeAsSeed;
        private RandomFloatVariable Target { get { return (RandomFloatVariable)target; } }


        protected override void OnEnable()
        {
            base.OnEnable();
            _useTimeAsSeed = serializedObject.FindProperty("_useTimeAsSeed");
        }

        protected override void DrawValue()
        {
            // Call Value.get so the displayed value also gets updated
            var value = Target.Value;
            base.DrawValue();

            EditorGUILayout.PropertyField(_useTimeAsSeed);
            using (var scope = new EditorGUI.DisabledGroupScope(_useTimeAsSeed.boolValue))
            {
                var newSeed = EditorGUILayout.IntField("Seed", Target.Seed);
                if (newSeed != Target.Seed)
                {
                    Target.Seed = newSeed;
                }
            }
        }

        protected override void DrawClampedFields(bool disableWithReadOnly)
        {
            base.DrawClampedFields(false);
        }
    }
}
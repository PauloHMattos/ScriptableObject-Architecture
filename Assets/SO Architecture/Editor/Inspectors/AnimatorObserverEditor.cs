using System;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(AnimatorObserver), true)]
    public class AnimatorObserverEditor : UnityEditor.Editor
    {
        private AnimatorObserver Target { get { return (AnimatorObserver)target; } }

        public void OnEnable()
        {
        }

        public override void OnInspectorGUI()
        {
            if (Target.Animator == null)
            {
                return;
            }

            EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel);
            EditorGUI.indentLevel = 1;
            for (var i = 0; i < Target.Animator.parameters.Length; i++)
            {
                var animatorParameter = Target.Animator.parameters[i];
                Type variableType;
                switch (animatorParameter.type)
                {
                    case AnimatorControllerParameterType.Float:
                        variableType = typeof(FloatVariable);
                        break;

                    case AnimatorControllerParameterType.Int:
                        variableType = typeof(IntVariable);
                        break;

                    case AnimatorControllerParameterType.Bool:
                    case AnimatorControllerParameterType.Trigger:
                        variableType = typeof(BoolVariable);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (Target.variables.Count <= i)
                {
                    Target.variables.Add(null);
                }

                Target.variables[i] = (BaseVariable) EditorGUILayout.ObjectField(new GUIContent(animatorParameter.name),
                    Target.variables[i], variableType, false);
            }
            EditorGUI.indentLevel = 0;
        }
    }
}
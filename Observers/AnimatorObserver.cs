using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [ExecuteAlways]
    [RequireComponent(typeof(Animator))]
    public class AnimatorObserver : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        public List<BaseVariable> variables = new List<BaseVariable>();

        public Animator Animator
        {
            get
            {
                if (_animator == null)
                {
                    _animator = GetComponent<Animator>();
                }
                return _animator;
            }
        }


        public void OnEnable()
        {
            _animator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (!Application.isPlaying)
                return;

            for (var i = 0; i < variables.Count; i++)
            {
                if (Animator.parameters.Length <= i || variables[i] == null)
                {
                    continue;
                }

                var parameter = Animator.parameters[i];
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Float:
                        _animator.SetFloat(parameter.name, ((FloatVariable) variables[i]).Value);
                        break;

                    case AnimatorControllerParameterType.Int:
                        _animator.SetInteger(parameter.name, ((IntVariable) variables[i]).Value);
                        break;

                    case AnimatorControllerParameterType.Bool:
                        _animator.SetBool(parameter.name, ((BoolVariable) variables[i]).Value);
                        break;

                    case AnimatorControllerParameterType.Trigger:
                        var trigger = (variables[i] as BoolVariable);
                        if (trigger != null && trigger.Value)
                        {
                            _animator.SetTrigger(parameter.name);
                            trigger.Value = false;
                        }
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
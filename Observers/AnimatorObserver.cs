using System;
using System.Collections.Generic;
using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.Observers
{
    [ExecuteAlways]
    [RequireComponent(typeof(Animator))]
    public class AnimatorObserver : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        public List<BaseVariable> Variables = new List<BaseVariable>();

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

            for (var i = 0; i < Variables.Count; i++)
            {
                if (Animator.parameters.Length <= i || Variables[i] == null)
                {
                    continue;
                }

                var parameter = Animator.parameters[i];
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Float:
                        _animator.SetFloat(parameter.name, ((FloatVariable) Variables[i]).Value);
                        break;

                    case AnimatorControllerParameterType.Int:
                        _animator.SetInteger(parameter.name, ((IntVariable) Variables[i]).Value);
                        break;

                    case AnimatorControllerParameterType.Bool:
                        _animator.SetBool(parameter.name, ((BoolVariable) Variables[i]).Value);
                        break;

                    case AnimatorControllerParameterType.Trigger:
                        var trigger = (Variables[i] as BoolVariable);
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
using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class NumericObserver<TType, TVariable, TResponse> :
        BaseObserver<TType, TVariable, TResponse>
        where TType : struct, IComparable
        where TVariable : BaseVariable<TType>
        where TResponse : UnityEvent<TType>
    {
        [SerializeField] protected AnimationCurve _modifierCurve = new AnimationCurve();
        [SerializeField] private bool _constrain = false;
        [SerializeField] private bool _equals = false;
        [SerializeField] private bool _bigger = false;
        [SerializeField] private bool _smaller = false;

        private TType _previousValue;


        public override void OnVariableChanged()
        {
            if (_constrain)
            {
                var result = _previousValue.CompareTo(_variable.Value);
                if (_equals)
                {
                    if ((_bigger && result >= 0) || (_smaller && result <= 0))
                    {
                        base.OnVariableChanged();
                    }
                    else if (result == 0)
                    {
                        base.OnVariableChanged();
                    }
                }
                else
                {
                    if ((_bigger && result > 0) || (_smaller && result < 0))
                    {
                        base.OnVariableChanged();
                    }
                    else if (result != 0)
                    {
                        base.OnVariableChanged();
                    }
                }
            }
            base.OnVariableChanged();
            _previousValue = _variable.Value;
        }
    }
}
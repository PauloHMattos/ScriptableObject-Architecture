using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class NumericObserver<TType, TVariable, TReference, TResponse> : NumericObserver<TType, TVariable, TResponse>
        where TType : struct, IComparable
        where TVariable : BaseVariable<TType>
        where TReference : BaseReference<TType, TVariable>
        where TResponse : UnityEvent<TType>
    {
        [SerializeField] private TReference _comparationReference = default;

        protected override TType GetComparisonValue()
        {
            if (_comparationReference.IsValueDefined)
            {
                _previousValue = _comparationReference.Value;
            }
            return base.GetComparisonValue();
        }

        public override void OnVariableChanged()
        {
            base.OnVariableChanged();
        }
    }

    public abstract class NumericObserver<TType, TVariable, TResponse> :
        BaseObserver<TType, TVariable, TResponse>
        where TType : struct, IComparable
        where TVariable : BaseVariable<TType>
        where TResponse : UnityEvent<TType>
    {
        [SerializeField, HideInInspector] protected AnimationCurve _modifierCurve = AnimationCurve.Constant(0, 1, 1);
        [SerializeField, HideInInspector] protected bool _sample = true;
        [SerializeField, HideInInspector] private bool _constrain = false;
        [SerializeField, HideInInspector] private bool _equals = false;
        [SerializeField, HideInInspector] private bool _bigger = false;
        [SerializeField, HideInInspector] private bool _smaller = false;

        protected TType _previousValue;

        protected virtual TType GetComparisonValue()
        {
            var ret = _previousValue;
            _previousValue = _variable.Value;
            return ret;
        }

        protected virtual bool ShouldRaise()
        {
            
            if (_constrain)
            {
                var result = _variable.Value.CompareTo(GetComparisonValue());
                if (_equals)
                {
                    if ((_bigger && result >= 0) || (_smaller && result <= 0))
                    {
                        return true;
                    }
                    else if (result == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    if ((_bigger && result > 0) || (_smaller && result < 0))
                    {
                        return true;
                    }
                    else if (result != 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            return true;
        }

        public override void OnVariableChanged()
        {
            if (!ShouldRaise())
                return;

            base.OnVariableChanged();
        }
    }
}
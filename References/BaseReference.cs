using ScriptableObjectArchitecture.Variables;
using UnityEngine;

namespace ScriptableObjectArchitecture.References
{
    [System.Serializable]
    public class BaseReference<TBase, TVariable> : BaseReference where TVariable : BaseVariable<TBase>
    {
        public BaseReference() : this (default) { }
        public BaseReference(TBase baseValue)
        {
            _useConstant = true;
            _constantValue = baseValue;
        }

        [SerializeField]
        private bool _useConstant = false;
        [SerializeField]
        protected TBase _constantValue = default;
        [SerializeField]
        private TVariable _variable = default;
        public TVariable Variable
        {
            get => _variable;
            set
            {
                _useConstant = false;
                _variable = value;
            }
        }

        public TBase Value
        {
            get => (_useConstant || _variable is null) ? _constantValue : _variable.Value;
            set
            {
                if (!_useConstant && !(_variable is null))
                {
                    Variable.Value = value;
                }
                else
                {
                    _useConstant = true;
                    _constantValue = value;
                }
            }
        }
        public bool IsValueDefined => _useConstant || !(_variable is null);

        public bool UseConstant { get => _useConstant; set => _useConstant = value; }

        public BaseReference CreateCopy()
        {
            var copy = (BaseReference<TBase, TVariable>)System.Activator.CreateInstance(GetType());
            copy._useConstant = _useConstant;
            copy._constantValue = _constantValue;
            copy._variable = _variable;

            return copy;
        }
        
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    //Can't get property drawer to work with generic arguments
    public abstract class BaseReference
    {
    }
}
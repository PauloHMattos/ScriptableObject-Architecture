using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "Axis2DVariable.asset",
        menuName = SoArchitectureUtility.VARIABLE_SUBMENU + "2D Axis",
        order = 124)]
    public class Axis2DVariable : ReadOnlyVariable<Vector2>
    {
        [Group("Axis Config", "Transform Icon"), SerializeField]
        protected string _xAxisName = "Horizontal";
        [SerializeField]
        protected string _yAxisName = "Vertical";
        [SerializeField]
        protected bool _raw;

        public override bool Clampable => false;

        public override void Awake()
        {
            base.Awake();
            _readOnly = true;
            _resetWhenStart = false;
        }

        public override Vector2 Value
        {
            get
            {
                try
                {
                    if (_raw)
                    {
                        _value.x = Input.GetAxisRaw(_xAxisName);
                        _value.y = Input.GetAxisRaw(_yAxisName);
                    }
                    else
                    {
                        _value.x = Input.GetAxis(_xAxisName);
                        _value.y = Input.GetAxis(_yAxisName);
                    }
                }
                catch (System.ArgumentException)
                {
                    _value.x = float.NaN;
                    _value.y = float.NaN;
                    //Debug.LogException(e);
                }
                return _value;
            }
        }
    }
}
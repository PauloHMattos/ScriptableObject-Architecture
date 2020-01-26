using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "Axis2DVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "2D Axis",
        order = 124)]
    public class Axis2DVariable : Vector2Variable
    {
        [Group("Axis Config"), SerializeField]
        private string _xAxisName = "Horizontal";
        [SerializeField]
        private string _yAxisName = "Vertical";
        [SerializeField]
        private bool _raw = false;

        public override bool Clampable { get { return false; } }

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
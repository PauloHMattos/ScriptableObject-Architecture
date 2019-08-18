using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "TimeVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Time",
        order = 124)]
    public class TimeVariable : ReadOnlyFloatVariable
    {
        [SerializeField]
        private int _seed;
        [SerializeField]
        private TimeVariableType _timeType;

        public override bool Clampable { get { return false; } }

        public override float Value
        {
            get
            {
                switch (_timeType)
                {
                    case TimeVariableType.DeltaTime:
                        _value = Time.deltaTime;
                        break;
                    case TimeVariableType.Time:
                        _value = Time.time;
                        break;
                    case TimeVariableType.FixedDeltaTime:
                        _value = Time.fixedDeltaTime;
                        break;
                    case TimeVariableType.FixedTime:
                        _value = Time.fixedTime;
                        break;
                    case TimeVariableType.RealTimeSinceStartup:
                        _value = Time.realtimeSinceStartup;
                        break;
                }
                return _value;
            }
        }

        public TimeVariableType TimeType { get => _timeType; set => _timeType = value; }

        public enum TimeVariableType
        {
            DeltaTime,
            Time,
            FixedDeltaTime,
            FixedTime,
            RealTimeSinceStartup,
        }
    }
}
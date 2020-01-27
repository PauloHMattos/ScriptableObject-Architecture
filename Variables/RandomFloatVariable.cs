using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    [CreateAssetMenu(
        fileName = "RandomFloatVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Random/float",
        order = 124)]
    public class RandomFloatVariable : ReadOnlyFloatVariable
    {
        [SerializeField, HideInInspector]
        private bool _useTimeAsSeed;
        [SerializeField, HideInInspector]
        private int _seed;
        [SerializeField, HideInInspector]
        private Random.State _state;

        public override bool Clampable { get { return true; } }

        public override float Value
        {
            get
            {
                Random.state = _state;
                if (IsClamped)
                {
                    _value = Random.Range(MinClampValue, MaxClampValue);
                }
                else
                {
                    _value = Random.value;
                }
                _state = Random.state;
                return _value;
            }
        }

        public int Seed
        {
            get
            {
                return _seed;
            }
            set
            {
                _seed = value;
                Random.InitState(_seed);
                _state = Random.state;
            }
        }

        public bool UseTimeAsSeed
        {
            get { return _useTimeAsSeed; }
            set { _useTimeAsSeed = value; }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            if (_useTimeAsSeed)
            {
                Seed = System.Environment.TickCount;
            }
            else
            {
                Seed = _seed;
            }
        }
    }
}
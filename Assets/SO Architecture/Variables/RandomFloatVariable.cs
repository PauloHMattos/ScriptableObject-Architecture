using UnityEngine;

namespace ScriptableObjectArchitecture
{

    [CreateAssetMenu(
        fileName = "RandomFloatVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Random/float",
        order = 124)]
    public class RandomFloatVariable : FloatVariable
    {
        [SerializeField]
        private int _seed;
        [SerializeField]
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

        public override bool ReadOnly
        {
            get
            {
                return _readOnly;
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Seed = _seed;
        }


        public override void Awake()
        {
            base.Awake();
            _readOnly = true;
            _resetWhenStart = false;
        }
    }
}
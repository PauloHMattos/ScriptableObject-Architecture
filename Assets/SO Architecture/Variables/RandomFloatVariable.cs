using UnityEngine;

namespace ScriptableObjectArchitecture
{

    [CreateAssetMenu(
        fileName = "RandomFloatVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Random float")]
    public class RandomFloatVariable : ReadOnlyVariable<float, RandomFloatVariable>
    {
        public override bool Clampable { get { return true; } }
        public override float Value
        {
            get
            {
                if (IsClamped)
                {
                    _value = Random.Range(MinClampValue, MaxClampValue);
                }
                else
                {
                    _value = Random.value;
                }
                return _value;
            }
        }
    }
}
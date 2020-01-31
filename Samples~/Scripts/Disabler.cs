using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Collections;
using ScriptableObjectArchitecture.Utility;
using ScriptableObjectArchitecture.References;
using UnityEngine;

namespace ScriptableObjectArchitecture.Samples.Scripts
{
    public class Disabler : SOArchitectureBaseMonoBehaviour
    {
        [Group("General")]
        public GameObjectCollection TargetSet;
        public GameObjectReference LastDisabled;


        [Button]
        public void DisableRandom()
        {
            if (TargetSet.Count > 0)
            {
                var index = Random.Range(0, TargetSet.Count);
                var objToDisable = TargetSet[index];
                objToDisable.SetActive(false);
                LastDisabled.Value = objToDisable;
            }
        }

        [Button]
        public void EnableAll()
        {
            for (int index = 0; index < TargetSet.Count; index++)
            {
                TargetSet[index].SetActive(true);
            }
        }
    }
}
using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Collections;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Samples.Scripts
{
    public class Disabler : SOArchitectureBaseMonoBehaviour
    {
        [Group("General")]
        public GameObjectCollection TargetSet;

        [Button]
        public void DisableRandom()
        {
            if (TargetSet.Count > 0)
            {
                var index = Random.Range(0, TargetSet.Count);

                var objToDisable = TargetSet[index];
                objToDisable.SetActive(false);
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
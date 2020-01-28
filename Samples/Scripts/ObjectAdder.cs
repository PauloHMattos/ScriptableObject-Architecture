using ScriptableObjectArchitecture.Collections;
using UnityEngine;

namespace ScriptableObjectArchitecture.Samples.Pong.Scripts
{
    public class ObjectAdder : MonoBehaviour
    {
        [SerializeField]
        private GameObjectCollection _targetCollection = default;

        private void OnEnable()
        {
            _targetCollection.Add(gameObject);
        }
        private void OnDisable()
        {
            _targetCollection.Remove(gameObject);
        }
    }
}
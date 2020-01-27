using System.Collections;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;
using Type = System.Type;

namespace ScriptableObjectArchitecture.Collections
{
    public abstract class BaseCollection : SoArchitectureBaseObject, IEnumerable
    {

#if UNITY_EDITOR
#pragma warning disable 0414
        [SerializeField]
        private bool _showCollectionItems = false;
#pragma warning restore
#endif

        public object this[int index]
        {
            get => List[index];
            set => List[index] = value;
        }

        public int Count => List.Count;

        public abstract IList List { get; }
        public abstract Type Type { get; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
        public bool Contains(object obj)
        {
            return List.Contains(obj);
        }
	}
}
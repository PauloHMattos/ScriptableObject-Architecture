using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public class GameObjectPool : MonoBehaviour
    {
        public bool _allowGrowth = true;
        public float _growthFactor = 1.5f;
        public GameObject _prefab;
        public int _poolCapacity = 10;
        public bool _defaultState = false;

        [SerializeField]
        private GameObjectCollection _pool;

        protected GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }

        public GameObject GetGameObject()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeInHierarchy)
                {
                    return _pool[i];
                }
            }

            // Add more objects to pool
            if (_allowGrowth)
            {
                GameObject obj = null;
                for (int i = 0; i < _poolCapacity * _growthFactor; i++)
                {
                    InstantiateToPool();
                }
                return obj;
            }
            else
            {
                Debug.LogWarning("Unable to get object from pool");
            }
            return null;
        }

        private void OnEnable()
        {
            if (_prefab == null)
            {
                return;
            }

            if (_pool == null)
            {
                _pool = ScriptableObject.CreateInstance<GameObjectCollection>();
            }

            for (int i = 0; i < _poolCapacity; i++)
            {
                InstantiateToPool();
            }
        }

        private void InstantiateToPool()
        {
            var obj = Instantiate(_prefab);
            obj.SetActive(_defaultState);
            _pool.Add(obj);
        }
    }
}

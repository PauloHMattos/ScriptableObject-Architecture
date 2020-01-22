using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "GameObjectPool.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "/Pools/GameObject",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
    public class GameObjectPool : GameObjectCollection
    {
        public bool _allowGrowth = true;
        public float _growthFactor = 1.5f;
        public GameObjectReference _prefab;
        public int _poolCapacity = 10;
        public bool _defaultState = false;

        public GameObject GetGameObject()
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (!_list[i].activeInHierarchy)
                {
                    return _list[i];
                }
            }

            // Add more objects to pool
            if (_allowGrowth)
            {
                int index = _list.Count;
                for (int i = 0; i < _poolCapacity * _growthFactor; i++)
                {
                    InstantiateToPool();
                }
                return _list[index];
            }
            else
            {
                throw new System.InvalidOperationException("Unable to get object from pool. Activate allowGrowth");
            }
        }

        private void OnEnable()
        {
            Clear();
            if (!_prefab.IsValueDefined || !Application.isPlaying)
            {
                return;
            }

            for (int i = 0; i < _poolCapacity; i++)
            {
                InstantiateToPool();
            }
        }
        /*
        private void OnDisable()
        {
            Debug.Log("OnDisable");
            Clear();
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            Clear();
        }
        */

        private void InstantiateToPool()
        {
            var obj = Instantiate(_prefab.Value);
            obj.SetActive(_defaultState);
            _list.Add(obj);
        }

        public override void Clear()
        {
            for (int i = _list.Count - 1; i >= 0; i--)
            {
                if (_list[i] != null)
                {
                    Destroy(_list[i]);
                }
            }

            base.Clear();
        }
    }
}

using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "GameObjectPool.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Pools/GameObject",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
    public class GameObjectPool : GameObjectCollection
    {
        public bool _allowGrowth = true;
        public float _growthFactor = 1.5f;
        public GameObject _prefab;
        public int _poolCapacity = 10;
        public bool _defaultState = false;

        protected GameObject Prefab
        {
            get
            {
                return _prefab;
            }
        }

        public GameObject GetGameObject()
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (!_list[i].activeInHierarchy)
                {
                    return _list[i];
                }
            }

            // Add more objects to pool
            if (_allowGrowth)
            {
                GameObject obj = null;
                for (int i = 0; i < _poolCapacity * _growthFactor; i++)
                {
                    obj = Instantiate(_prefab);
                    obj.SetActive(_defaultState);
                    Add(obj);
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

            for (int i = 0; i < _poolCapacity; i++)
            {
                var obj = Instantiate(_prefab);
                obj.SetActive(_defaultState);
                Add(obj);
            }
        }
    }
}

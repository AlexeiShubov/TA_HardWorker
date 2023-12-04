using System.Collections.Generic;
using AxGrid.Base;
using STIGRADOR;
using UnityEngine;

namespace ClassesTools
{
    public class DynamicCollection : MonoBehaviourExtBind
    {
        [SerializeField] protected string _collectionName;
        [SerializeField] protected CollectionObject _collectionObjectPrefab;
        [SerializeField] protected Transform _parent;
        
        private PoolMonoFactory<CollectionObject> _factory;

        protected ObjectPool<CollectionObject> _pool;
        protected List<CollectionObject> _activeCollectionPrefabs;

        public Transform Parent => _parent;
        public List<CollectionObject> ActiveCollectionPrefabs => _activeCollectionPrefabs;

        [OnAwake]
        protected void CustomAwake()
        {
            _factory = new PoolMonoFactory<CollectionObject>(_collectionObjectPrefab, _parent);
            _pool = new BaseMonoPool<CollectionObject>(_factory);
            _activeCollectionPrefabs = new List<CollectionObject>();
        }

        [OnStart]
        protected void CustomStart()
        {
            if (string.IsNullOrEmpty(_collectionName))
            {
                _collectionName = "CollectionContent";
            }

            Model.EventManager.AddAction<List<CollectionData>>($"On{_collectionName}Changed", OnCollectionChanged);
        }

        protected virtual void OnCollectionChanged(List<CollectionData> collection)
        {
            Debug.LogError("--------------------------------------------------");

            var countNewCollectionObjects = collection.Count - _activeCollectionPrefabs.Count;
Debug.LogError(countNewCollectionObjects + " " +  _collectionName);
            if (countNewCollectionObjects > 0)
            {
                for (var i = 0; i < countNewCollectionObjects; i++)
                {
                    _activeCollectionPrefabs.Add((CollectionObject) _pool.GetObject());
                }
            }

            if (countNewCollectionObjects < 0)
            {
                for (var i = Mathf.Abs(countNewCollectionObjects); i < _activeCollectionPrefabs.Count; i++)
                {
                    _activeCollectionPrefabs[i].Return();
                    _activeCollectionPrefabs.Remove(_activeCollectionPrefabs[i]);
                }
            }
            
            for (var i = 0; i < collection.Count; i++)
            {
                if (collection.Count != _activeCollectionPrefabs.Count)
                {
                    Debug.LogError("Ты где-то прое**ался. Опять!!!");
                }
                
                _activeCollectionPrefabs[i].Init(collection[i]);
            }
            
            if(_activeCollectionPrefabs.Count == 0) return;
            
            _activeCollectionPrefabs[^1].transform.SetAsLastSibling();
        }
    }
}
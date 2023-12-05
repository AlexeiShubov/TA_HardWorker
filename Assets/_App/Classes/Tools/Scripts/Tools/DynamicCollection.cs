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
            var differenceBetweenTwoCollections = collection.Count - _activeCollectionPrefabs.Count;

            if (differenceBetweenTwoCollections > 0)
            {
                for (var i = 0; i < differenceBetweenTwoCollections; i++)
                {
                    var newCollectionObject = (CollectionObject) _pool.GetObject();
                    
                    _activeCollectionPrefabs.Add(newCollectionObject);
                    newCollectionObject.transform.localPosition = Vector3.zero;
                }
            }

            if (differenceBetweenTwoCollections < 0)
            {
                for (var i = Mathf.Abs(differenceBetweenTwoCollections); i > 0; i--)
                {
                    _activeCollectionPrefabs[^i].Return();
                    _activeCollectionPrefabs.Remove(_activeCollectionPrefabs[^i]);
                }
            }

            for (var i = 0; i < collection.Count; i++)
            {
                _activeCollectionPrefabs[i].Init(collection[i]);
            }
            
            if(_activeCollectionPrefabs.Count == 0) return;
            
            _activeCollectionPrefabs[^1].transform.SetAsLastSibling();
        }
    }
}
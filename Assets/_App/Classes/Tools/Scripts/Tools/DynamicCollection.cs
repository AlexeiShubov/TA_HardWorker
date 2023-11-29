using System.Collections.Generic;
using AxGrid.Base;
using STIGRADOR;
using UnityEngine;

namespace ClassesTools
{
    public class DynamicCollection : MonoBehaviourExtBind
    {
        [SerializeField] private string _collectionName;
        [SerializeField] private CollectionObject _collectionObjectPrefab;
        [SerializeField] private Transform _parent;

        private ObjectPool<CollectionObject> _pool;
        private PoolMonoFactory<CollectionObject> _factory;
        private List<CollectionObject> _activeCollectionPrefabs;

        [OnAwake]
        private void CustomAwake()
        {
            _factory = new PoolMonoFactory<CollectionObject>(_collectionObjectPrefab, _parent);
            _pool = new BaseMonoPool<CollectionObject>(_factory);
            _activeCollectionPrefabs = new List<CollectionObject>();
        }

        [OnStart]
        private void CustomStart()
        {
            if (string.IsNullOrEmpty(_collectionName))
            {
                _collectionName = "CollectionContent";
            }

            Model.EventManager.AddAction<List<CollectionData>>($"On{_collectionName}Changed", OnCollectionChanged);
        }

        private void OnCollectionChanged(List<CollectionData> collection)
        {
            var countIterations = collection.Count;

            foreach (var collectionObject in _activeCollectionPrefabs)
            {
                collectionObject.Return();
            }

            _activeCollectionPrefabs.Clear();

            for (var i = 0; i < countIterations; i++)
            {
                var newObject = (CollectionObject) _pool.GetObject();

                newObject.Init(collection[i]);
                _activeCollectionPrefabs.Add(newObject);
            }
        }
    }
}
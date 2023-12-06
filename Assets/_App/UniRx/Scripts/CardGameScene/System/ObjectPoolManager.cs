using System.Collections.Generic;
using ClassesTools;
using STIGRADOR;
using UnityEngine;

namespace UniRxTask
{
    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField] private CollectionObject _collectionObjectPrefab;

        private List<CollectionObject> _activeObjects;
        private PoolMonoFactory<CollectionObject> _factory;
        private ObjectPool<CollectionObject> _pool;

        private void Awake()
        {
            _activeObjects = new List<CollectionObject>();
            _factory = new PoolMonoFactory<CollectionObject>(_collectionObjectPrefab, transform);
            _pool = new BaseMonoPool<CollectionObject>(_factory);
        }

        public CollectionObject GetCollectionObject()
        {
            var newCollectionObject = (CollectionObject) _pool.GetObject();
            _activeObjects.Add(newCollectionObject);

            return newCollectionObject;
        }

        public CollectionObject FindObjectWithID(int ID)
        {
            return _activeObjects.Find(t => t.Data.ID == ID);
        }

        public void ReturnObject(CollectionObject collectionObject)
        {
            _pool.ReturnObject(collectionObject);
            _activeObjects.Remove(collectionObject);
        }
    }
}
using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;
using UniRxTask;
using UnityEngine;

namespace ClassesTools
{
    public class DynamicCollection : MonoBehaviourExtBind
    {
        [SerializeField] private bool _spawningCollection;
        [SerializeField] private string _collectionName;
        [SerializeField] private ObjectPoolManager _objectPoolManager;
        [SerializeField] private Transform _parent;

        [Space(5)] [Tooltip("Animation settings")] 
        [SerializeField] private float _delayCollectionMoving = 0.5f;
        [SerializeField] private float _collectionObjectOffset = 1.25f;
        
        private ICollectionAnimator _animator;
        private List<CollectionObject> _activeCollectionPrefabs;

        public Transform Parent => _parent;
        public List<CollectionObject> ActiveCollectionPrefabs => _activeCollectionPrefabs;

        [OnAwake]
        protected void CustomAwake()
        {
            _activeCollectionPrefabs = new List<CollectionObject>();
            _animator = new CollectionHorizontalMover(_delayCollectionMoving, _collectionObjectOffset, this);
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

        [Bind]
        private void CreateNewCollectionObject(CollectionData collectionData)
        {
            if(!_spawningCollection) return;
            
            var newCollectionObject = _objectPoolManager.GetCollectionObject();
            
            _activeCollectionPrefabs.Add(newCollectionObject);
            newCollectionObject.transform.SetParent(_parent);
            newCollectionObject.transform.localPosition = Vector3.zero;
            newCollectionObject.Init(collectionData);
            newCollectionObject.MoveTo(GetTargetPositionForCollectionObject());
            _animator.MoveCollectionTransform(CreateNewPath());
        }

        [Bind]
        private void OnCollectionChanged(List<CollectionData> newCollectionDatas)
        {
            foreach (var data in newCollectionDatas)
            {
                
            }
            
            return;
            var differenceBetweenTwoCollections = newCollectionDatas.Count - _activeCollectionPrefabs.Count;

            if (differenceBetweenTwoCollections > 0)
            {
                for (var i = 0; i < differenceBetweenTwoCollections; i++)
                {
                    /*var newCollectionObject = (CollectionObject) _objectPoolManager.GetObject();

                    _activeCollectionPrefabs.Add(newCollectionObject);
                    newCollectionObject.transform.localPosition = Vector3.zero;*/
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

            for (var i = 0; i < newCollectionDatas.Count; i++)
            {
                _activeCollectionPrefabs[i].Init(newCollectionDatas[i]);
            }

            if (_activeCollectionPrefabs.Count == 0) return;

            _activeCollectionPrefabs[^1].transform.SetAsLastSibling();
        }

        private Vector2 GetTargetPositionForCollectionObject()
        {
            return new Vector2((_activeCollectionPrefabs.Count - 1) * _collectionObjectOffset, 0f);
        }

        private void OnDisable()
        {
            Model.EventManager.RemoveAction<List<CollectionData>>($"On{_collectionName}Changed", OnCollectionChanged);
        }
    }
}
using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;
using UniRxTask;
using UnityEngine;

namespace ClassesTools
{
    public class DynamicCollection : MonoBehaviourExtBind
    {
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

            Model.EventManager.AddAction<List<CollectionData>, string>($"On{_collectionName}Changed", OnCollectionChanged);
        }

        [Bind]
        private void CreateNewCollectionObject(CollectionData collectionData, string collectionName)
        {
            if(collectionName != _collectionName) return;
            
            var newCollectionObject = _objectPoolManager.GetCollectionObject();
            
            _activeCollectionPrefabs.Add(newCollectionObject);
            newCollectionObject.transform.SetParent(_parent);
            newCollectionObject.transform.localPosition = Vector3.zero;
            newCollectionObject.Init(collectionData);
            newCollectionObject.MoveTo(GetTargetPositionForCollectionObject());
            _animator.MoveCollectionTransform(CreateNewPath());
        }

        [Bind]
        private void OnCollectionChanged(List<CollectionData> newCollectionDatas, string collectionName)
        {
            if(collectionName != _collectionName) return;

            _activeCollectionPrefabs.Clear();
            
            foreach (var data in newCollectionDatas)
            {
                var obj = _objectPoolManager.FindObjectWithID(data.ID);

                obj.transform.parent = transform;
                obj.Init(data);
                _activeCollectionPrefabs.Add(obj);
                obj.MoveTo(GetTargetPositionForCollectionObject());
            }
            
            _animator.MoveCollectionTransform(CreateNewPath());
        }

        private Vector2 GetTargetPositionForCollectionObject()
        {
            return new Vector2((_activeCollectionPrefabs.Count - 1) * _collectionObjectOffset, 0f);
        }

        private void OnDisable()
        {
            Model.EventManager.RemoveAction<List<CollectionData>, string>($"On{_collectionName}Changed", OnCollectionChanged);
        }
    }
}
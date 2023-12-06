using AxGrid.Path;
using ClassesTools;
using UnityEngine;

namespace UniRxTask
{
    public class CollectionHorizontalMover : ICollectionAnimator
    {
        private readonly string _collectionName;
        private readonly float _delay;
        private readonly float _collectionObjectOffset;
        private readonly DynamicCollection _collection;
        
        public CollectionHorizontalMover(float delay, float collectionObjectOffset, DynamicCollection collection)
        {
            _delay = delay;
            _collectionObjectOffset = collectionObjectOffset;
            _collection = collection;
        }

        public void MoveCollectionTransform(CPath cPath)
        {
            if (_collection.ActiveCollectionPrefabs is null || _collection.ActiveCollectionPrefabs.Count == 0) return;

            MoveParent(cPath, GetTargetPosition(), _collection.Parent);
        }

        private void MoveParent(CPath cPath, Vector2 targetPosition, Transform collection)
        {
            MoveObject(cPath, collection, collection.localPosition, targetPosition);
        }

        private void MoveObject(CPath cPath, Transform transform, Vector2 startPosition, Vector2 targetLocalPosition)
        {
            cPath.EasingQuadEaseOut(_delay, 0f, 1f, t => { transform.localPosition = Vector2.Lerp(startPosition, targetLocalPosition, t); });
        }

        private Vector2 GetTargetPosition()
        {
            return new Vector2((_collection.ActiveCollectionPrefabs.Count - 1) * _collectionObjectOffset * -0.5f, _collection.Parent.localPosition.y);
        }
    }
}
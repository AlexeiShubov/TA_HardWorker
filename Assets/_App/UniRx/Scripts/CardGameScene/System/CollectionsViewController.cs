using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Path;
using UniRxTask;
using UnityEngine;

public class CollectionsViewController : MonoBehaviourExtBind
{
    private const string _TOP_COLLECTION = "TopCollection";
    private const string _BOTTOM_COLLECTION = "BottomCollection";
    
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _offsetX = 1.25f;
    [SerializeField] private DynamicCollectionTaskUniRx _topCollections;
    [SerializeField] private DynamicCollectionTaskUniRx _bottomCollection;

    private Dictionary<string, DynamicCollectionTaskUniRx> _collectionsMap;

    [OnAwake]
    private void CustomAwake()
    {
        _collectionsMap = new Dictionary<string, DynamicCollectionTaskUniRx>
        {
            {_TOP_COLLECTION, _topCollections},
            {_BOTTOM_COLLECTION, _bottomCollection}
        };
    }

    public void OnCollectionChanged(string collectionName)
    {
        var collection = _collectionsMap[collectionName];
        var collectionObject = collection.ActiveCollectionPrefabs[^1].transform;
        var targetLocalPosition = GetTargetPositionForCollectionObject(collection.ActiveCollectionPrefabs.Count);

        MoveCollectionObject(collectionObject, targetLocalPosition);
        MoveParent(new Vector2(-targetLocalPosition.x * 0.5f, collection.Parent.localPosition.y), collection.Parent);
    }

    private void MoveCollectionObject(Transform transform, Vector2 targetPosition)
    {
        MoveObject(CreateNewPath(), transform, Vector2.zero, targetPosition);
    }

    private void MoveParent(Vector2 targetPosition, Transform collection)
    {
        MoveObject(CreateNewPath(), collection, collection.localPosition, targetPosition);
    }

    private void MoveObject(CPath cPath, Transform transform, Vector2 startPosition, Vector2 targetLocalPosition)
    {
        cPath.EasingQuadEaseOut(_delay, 0f, 1f,
            t => { transform.localPosition = Vector2.Lerp(startPosition, targetLocalPosition, t); });
    }

    private Vector2 GetTargetPositionForCollectionObject(int collectionLength)
    {
        return new Vector2((collectionLength - 1) * _offsetX, 0f);
    }
}

using System.Collections.Generic;
using System.Linq;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using ClassesTools;
using Task2;
using UnityEngine;

public class Test : MonoBehaviourExtBind
{
    [SerializeField] private float _delay = 1f;
    [SerializeField] private float _offsetX = 1.25f;
    [SerializeField] private Transform _parent;
    [SerializeField] private CollectionObjectTaskTwo _prefab;

    private List<CollectionObjectTaskTwo> _cards;
    private List<CPath> _cPaths;

    private CPath _cPathParent;

    [OnAwake]
    private void CustomAwake()
    {
        _cards = new List<CollectionObjectTaskTwo>();
        _cPaths = new List<CPath>();
        _cPathParent = CreateNewPath();
    }

    [OnUpdate]
    private void CustomUpdate()
    {
        _cPathParent.Update(Time.deltaTime);
    }

    [Bind("OnAddNewCardButtonClick")]
    private void StartMove()
    {
        var newCardTransform = CreateNewCard();
        var targetPosition = GetTargetPositionForNewCard();
        
        _cPaths.Add(CreateNewPath());
        MoveObject(CreateNewPath(), newCardTransform, Vector2.zero, targetPosition);
        _cPathParent.Clear();
        _cPathParent = CPath.Create();
        MoveObject(_cPathParent, _parent, _parent.localPosition, new Vector2(-targetPosition.x * 0.5f, _parent.localPosition.y));
    }

    [Bind]
    private void OnCollectionObjectClick(CollectionObjectTaskTwo collectionObject)
    {
        var card = RemoveCard(collectionObject);
        Destroy(card.gameObject);
        UpdateInfoCards();
        
        _cPathParent.Clear();
        _cPathParent = CPath.Create();
        MoveObject(_cPathParent, _parent, _parent.localPosition, new Vector2(-GetTargetPositionForNewCard().x * 0.5f, _parent.localPosition.y));
    }

    private Transform CreateNewCard()
    {
        var newCard = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _parent);

        newCard.transform.localPosition = Vector3.zero;
        _cards.Add(newCard);
        newCard.Init(new CollectionData(_cards.Count, name), _cards.Count);
        
        return newCard.transform;
    }

    private CollectionObjectTaskTwo RemoveCard(CollectionObjectTaskTwo collectionObject)
    {
        var info = collectionObject.Data.info;
        var card = _cards.First(t => t.Data.info == info);

        _cards.Remove(card);

        return card;
    }

    private void UpdateInfoCards()
    {
        for (var i = 0; i < _cards.Count; i++)
        {
            _cards[i].Init(new CollectionData(i + 1, name), i);
            _cards[i].transform.localPosition = new Vector2(i * _offsetX, 0f);
        }
    }

    private Vector2 GetTargetPositionForNewCard()
    {
        return new Vector2((_cards.Count - 1) * _offsetX, 0f);
    }

    private void MoveObject(CPath cPath, Transform transform, Vector2 startPosition,Vector2 targetLocalPosition)
    {
        cPath.EasingQuadEaseOut(_delay, 0f, 1f, t =>
        {
            transform.localPosition = Vector2.Lerp(startPosition, targetLocalPosition, t);
        }).Action(() =>
        {
            UpdateInfoCards();
        });
    }
}

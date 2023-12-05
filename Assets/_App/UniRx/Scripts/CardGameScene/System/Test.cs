using System.Collections.Generic;
using System.Linq;
using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using ClassesTools;
using Task2;
using UnityEngine;

namespace UniRxTask
{
    public class Test : MonoBehaviourExtBind
    {
        [SerializeField] private float _delay = 1f;
        [SerializeField] private float _offsetX = 1.25f;
        [SerializeField] private Transform _parent;
        [SerializeField] private CollectionObjectTaskTwo _prefab;

        private List<CollectionObjectTaskTwo> _cards;

        [OnAwake]
        private void CustomAwake()
        {
            _cards = new List<CollectionObjectTaskTwo>();
        }
        
        public void OnAddNewCardButtonClick()
        {
            var newCardTransform = CreateNewCollectionObject();
            var targetPosition = GetTargetPositionForNewCollectionObject();

            MoveCollectionObject(newCardTransform, targetPosition);
            MoveParent(new Vector2(-targetPosition.x * 0.5f, _parent.localPosition.y));
        }

        private void MoveCollectionObject(Transform transform, Vector2 targetPosition)
        {
            MoveObject(CreateNewPath(), transform, Vector2.zero, targetPosition);
        }

        /*[Bind]
        private void OnCollectionObjectClick(CollectionObjectTaskTwo collectionObject)
        {
            Debug.LogError("---------------");
            var card = RemoveCollectionObject(collectionObject);

            Destroy(card.gameObject);
            UpdateInfoCollectionObject();
            MoveParent(new Vector2(-GetTargetPositionForNewCollectionObject().x * 0.5f, _parent.localPosition.y));
        }*/

        private void MoveParent(Vector2 targetPosition)
        {
            Path.Clear();
            Path = CreateNewPath();
            MoveObject(Path, _parent, _parent.localPosition, targetPosition);
        }

        private Transform CreateNewCollectionObject()
        {
            var newCollectionObject = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _parent);

            newCollectionObject.transform.localPosition = Vector3.zero;
            _cards.Add(newCollectionObject);
            newCollectionObject.Init(new CollectionData(_cards.Count, name, _cards.Count));

            return newCollectionObject.transform;
        }

        private CollectionObjectTaskTwo RemoveCollectionObject(CollectionObjectTaskTwo collectionObject)
        {
            var info = collectionObject.Data.info;
            var card = _cards.First(t => t.Data.info == info);

            _cards.Remove(card);

            return card;
        }

        private void UpdateInfoCollectionObject()
        {
            for (var i = 0; i < _cards.Count; i++)
            {
                _cards[i].Init(new CollectionData(i + 1, name, i + 1));
                _cards[i].transform.localPosition = new Vector2(i * _offsetX, 0f);
            }
        }

        private Vector2 GetTargetPositionForNewCollectionObject()
        {
            return new Vector2((_cards.Count - 1) * _offsetX, 0f);
        }

        private void MoveObject(CPath cPath, Transform transform, Vector2 startPosition, Vector2 targetLocalPosition)
        {
            cPath.EasingQuadEaseOut(_delay, 0f, 1f,
                    t => { transform.localPosition = Vector2.Lerp(startPosition, targetLocalPosition, t); })
                .Action(UpdateInfoCollectionObject);
        }
    }
}
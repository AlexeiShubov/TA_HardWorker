using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using ClassesTools;
using UnityEngine;

namespace Task2
{
    [State("GameState")]
    public class GameState : FSMState
    {
        private const string TOP_COLLECTION = "TopCollection";
        private const string BOTTOM_COLLECTION = "BottomCollection";

        private readonly List<CollectionData> _topCollection;
        private readonly List<CollectionData> _bottomCollection;

        public GameState()
        {
            _topCollection = new List<CollectionData>();
            _bottomCollection = new List<CollectionData>();
        }

        [Bind]
        private void OnBtn(string buttonName)
        {
            if (buttonName == "AddNewCardButton")
            {
                CreateNewCard();
            }
        }

        [Bind]
        private void OnCollectionObjectClick(CollectionObjectTaskTwo collectionObjectTaskTwo)
        {
            var currentCollectionName = collectionObjectTaskTwo.Data.CurrentCollectionName;
            var targetCollection = currentCollectionName == BOTTOM_COLLECTION ? _bottomCollection : _topCollection;

            for (var i = 0; i < targetCollection.Count; i++)
            {
                if (targetCollection[i] == collectionObjectTaskTwo.Data)
                {
                    ChangeCollectionElement(targetCollection, currentCollectionName == BOTTOM_COLLECTION ? _topCollection : _bottomCollection, targetCollection[i]);
                    return;
                }
            }
        }

        private void ChangeCollectionElement(List<CollectionData> from, List<CollectionData> too, CollectionData collectionData)
        {
            from.Remove(collectionData);
            too.Add(collectionData);
            collectionData.CurrentCollectionName = collectionData.CurrentCollectionName == TOP_COLLECTION
                ? BOTTOM_COLLECTION
                : TOP_COLLECTION;
            
            Model.Set(BOTTOM_COLLECTION, _bottomCollection).Refresh(BOTTOM_COLLECTION);
            Model.Set(TOP_COLLECTION, _topCollection).Refresh(TOP_COLLECTION);
        }

        private void CreateNewCard()
        {
            ChangeBottomCollection();

            if (!Model.ContainsKey(BOTTOM_COLLECTION))
            {
                Model.Set(BOTTOM_COLLECTION, _bottomCollection);
            }
            else
            {
                Model.Set(BOTTOM_COLLECTION, _bottomCollection).Refresh(BOTTOM_COLLECTION);
            }
        }

        private void ChangeBottomCollection()
        {
            _bottomCollection.Add(new CollectionData($"{Random.value:F2}", BOTTOM_COLLECTION));
        }
    }
}
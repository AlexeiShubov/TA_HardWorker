using System.Collections.Generic;
using System.Linq;
using AxGrid.FSM;
using AxGrid.Model;
using ClassesTools;
using UnityEngine;

namespace Task2
{
    [State("GameState")]
    public class GameState : FSMState
    {
        private const string _TOP_COLLECTION = "TopCollection";
        private const string _BOTTOM_COLLECTION = "BottomCollection";
        private const string _MIDLE_COLLECTION = "MidleCollection";

        private readonly List<CollectionData> _topCollectionData;
        private readonly List<CollectionData> _bottomCollectionData;
        private readonly List<CollectionData> _midleCollectionData;

        private readonly Dictionary<string, List<CollectionData>> _collectionDataMap;

        public GameState()
        {
            _topCollectionData = new List<CollectionData>();
            _bottomCollectionData = new List<CollectionData>();
            _midleCollectionData = new List<CollectionData>();
            
            _collectionDataMap = new Dictionary<string, List<CollectionData>>
            {
                {_TOP_COLLECTION, _topCollectionData},
                {_BOTTOM_COLLECTION, _bottomCollectionData},
                {_MIDLE_COLLECTION, _midleCollectionData}
            };
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
            var currentCollectionData = collectionObjectTaskTwo.Data;
            var currentCollectionName = currentCollectionData.CurrentCollectionName;
            var targetCollectionName = GetNewCollectionNameForSwap(currentCollectionName);
            var currentCollection = _collectionDataMap[currentCollectionName];
            var targetCollection = _collectionDataMap[targetCollectionName];

            ChangeCollectionElement(currentCollection, targetCollection, currentCollectionData, targetCollectionName);
            UpdateCollectionsInTheModel();
        }

        private string GetNewCollectionNameForSwap(string currentCollectionName)
        {
            var listCollections = (from item 
                in _collectionDataMap 
                where item.Key != currentCollectionName 
                select item.Key).ToArray();

            return listCollections[Random.Range(0, listCollections.Length)];
        }

        private void ChangeCollectionElement(List<CollectionData> from, List<CollectionData> too, CollectionData collectionData, string newCollectionNameData)
        {
            from.Remove(collectionData);
            too.Add(collectionData);
            collectionData.CurrentCollectionName = newCollectionNameData;
        }

        private void CreateNewCard()
        {
            ChangeBottomCollection();

            if (!Model.ContainsKey(_BOTTOM_COLLECTION))
            {
                Model.Set(_BOTTOM_COLLECTION, _bottomCollectionData);
            }
            else
            {
                Model.Set(_BOTTOM_COLLECTION, _bottomCollectionData).Refresh(_BOTTOM_COLLECTION);
            }
        }

        private void UpdateCollectionsInTheModel()
        {
            Model.Set(_BOTTOM_COLLECTION, _bottomCollectionData).Refresh(_BOTTOM_COLLECTION);
            Model.Set(_TOP_COLLECTION, _topCollectionData).Refresh(_TOP_COLLECTION);
            Model.Set(_MIDLE_COLLECTION, _midleCollectionData).Refresh(_MIDLE_COLLECTION);
        }

        private void ChangeBottomCollection()
        {
            _bottomCollectionData.Add(new CollectionData(Random.Range(0, 100), _BOTTOM_COLLECTION, 0));
        }
    }
}
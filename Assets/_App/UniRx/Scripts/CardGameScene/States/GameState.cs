using System.Collections.Generic;
using System.Linq;
using AxGrid.FSM;
using AxGrid.Model;
using ClassesTools;
using Task2;
using UnityEngine;

namespace UniRxTask
{
    [State("GameState")]
    public class GameState : FSMState
    {
        private const string _TOP_COLLECTION = "TopCollection";
        private const string _MIDLE_COLLECTION = "MidleCollection";
        private const string _BOTTOM_COLLECTION = "BottomCollection";

        private readonly List<CollectionData> _topCollectionData;
        private readonly List<CollectionData> _midleCollectionData;
        private readonly List<CollectionData> _bottomCollectionData;
        private readonly Dictionary<string, List<CollectionData>> _collectionDataMap;

        private readonly CollectionsViewController _collectionsViewController;

        public GameState(CollectionsViewController collectionsViewController)
        {
            _collectionsViewController = collectionsViewController;
            _topCollectionData = new List<CollectionData>();
            _midleCollectionData = new List<CollectionData>();
            _bottomCollectionData = new List<CollectionData>();
            
            _collectionDataMap = new Dictionary<string, List<CollectionData>>
            {
                {_TOP_COLLECTION, _topCollectionData},
                {_MIDLE_COLLECTION, _midleCollectionData},
                {_BOTTOM_COLLECTION, _bottomCollectionData}
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

            ChangeDataCollectionElement(currentCollection, targetCollection, currentCollectionData, targetCollectionName);
        }

        private string GetNewCollectionNameForSwap(string currentCollectionName)
        {
            var listCollections = (from item 
                in _collectionDataMap 
                where item.Key != currentCollectionName 
                select item.Key).ToArray();

            return listCollections[Random.Range(0, listCollections.Length)];
        }

        private void ChangeDataCollectionElement(List<CollectionData> from, List<CollectionData> too, CollectionData collectionData, string newCollectionNameData)
        {
            from.Remove(collectionData);
            too.Add(collectionData);

            UpdateCollectionData(from, collectionData.CurrentCollectionName);
            UpdateCollectionData(too, newCollectionNameData);
        }

        private void CreateNewCard()
        {
            var newID = _collectionDataMap.Sum(item => item.Value.Count);
            var newCollectionData = new CollectionData(newID, _bottomCollectionData.Count + 1, _BOTTOM_COLLECTION, _bottomCollectionData.Count + 1);
            _bottomCollectionData.Add(newCollectionData);

            Model.EventManager.Invoke("CreateNewCollectionObject", newCollectionData, _BOTTOM_COLLECTION);
        }

        private void UpdateCollectionData(List<CollectionData> collectionDatas, string collectionNameData)
        {
            for (var i = 0; i < collectionDatas.Count; i++)
            {
                collectionDatas[i].Priority = i;
                collectionDatas[i].CurrentCollectionName = collectionNameData;
            }
            
            UpdateCollectionInTheModel(collectionNameData, collectionDatas);
        }

        private void UpdateCollectionInTheModel(string collectionName, List<CollectionData> collectionDatas)
        {
            if (!Model.ContainsKey(collectionName))
            {
                Model.EventManager.Invoke($"On{collectionName}Changed", collectionDatas, collectionName);
            }
            else
            {
                Model.EventManager.Invoke($"On{collectionName}Changed", collectionDatas, collectionName);
            }
        }
    }
}

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
        private const string _BOTTOM_COLLECTION = "BottomCollection";

        private readonly List<CollectionData> _topCollectionData;
        private readonly List<CollectionData> _bottomCollectionData;
        private readonly Dictionary<string, List<CollectionData>> _collectionDataMap;

        private readonly CollectionsViewController _collectionsViewController;

        public GameState(CollectionsViewController collectionsViewController)
        {
            _collectionsViewController = collectionsViewController;
            _topCollectionData = new List<CollectionData>();
            _bottomCollectionData = new List<CollectionData>();
            
            _collectionDataMap = new Dictionary<string, List<CollectionData>>
            {
                {_TOP_COLLECTION, _topCollectionData},
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

        private void ChangeDataCollectionElement(List<CollectionData> from, List<CollectionData> too, CollectionData collectionData, string newCollectionNameData)
        {
            Debug.LogError(from.Count);
            from.Remove(collectionData);
            Debug.LogError(from.Count);
            too.Add(collectionData);
            collectionData.Priority = too.Count + 1;
            collectionData.CurrentCollectionName = newCollectionNameData;
        }

        private void CreateNewCard()
        {
            var newCollectionData = new CollectionData(_bottomCollectionData.Count + 1, _BOTTOM_COLLECTION, _bottomCollectionData.Count + 1);
            _bottomCollectionData.Add(newCollectionData);

            if (!Model.ContainsKey(_BOTTOM_COLLECTION))
            {
                Model.Set(_BOTTOM_COLLECTION, _bottomCollectionData);
            }
            else
            {
                Model.Set(_BOTTOM_COLLECTION, _bottomCollectionData).Refresh(_BOTTOM_COLLECTION);
            }
            
            _collectionsViewController.OnCollectionChanged(_BOTTOM_COLLECTION);
        }

        private void UpdateCollectionsInTheModel()
        {
            Model.Set(_BOTTOM_COLLECTION, _bottomCollectionData);
            Model.Set(_TOP_COLLECTION, _topCollectionData);
            
            /*_collectionsViewController.OnCollectionChanged(_BOTTOM_COLLECTION);
            _collectionsViewController.OnCollectionChanged(_TOP_COLLECTION);*/
        }
    }
}

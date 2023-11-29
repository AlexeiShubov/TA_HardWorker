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
                AddNewCard();
            }
        }
        
        private void AddNewCard()
        {
            ChangeCollection();

            if (!Model.ContainsKey(BOTTOM_COLLECTION))
            {
                Model.Set(BOTTOM_COLLECTION, _bottomCollection);
            }
            else
            {
                Model.Set(BOTTOM_COLLECTION, _bottomCollection).Refresh(BOTTOM_COLLECTION);
            }
        }

        private void ChangeCollection()
        {
            _bottomCollection.Add(new CollectionData($"{Random.value:F2}"));
        }
    }
}
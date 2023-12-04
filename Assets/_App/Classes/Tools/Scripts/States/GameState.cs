using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace ClassesTools
{
    [State(StateNames.GameState)]
    public class GameState : FSMState
    {
        private const int _MIN_VALUE_FOR_RANDOM_RANGE = 1;
        private const int _MAX_VALUE_FOR_RANDOM_RANGE = 25;

        private readonly List<CollectionData> _collection = new List<CollectionData>();

        [Enter]
        private void Enter()
        {
            Model.EventManager.Invoke(ProjectEvents.OnSettingsPanelActiveChanged, false);

            Model.Set(ButtonNames.BtnGameEnable, false);
            Model.Set(ButtonNames.BtnSettingsEnable, true);
            Model.Set(ButtonNames.BtnCollectionContentEnable, true);
        }

        [Bind]
        private void OnBtn(string buttonName)
        {
            switch (buttonName)
            {
                case ButtonNames.Settings:
                    Parent.Change(StateNames.SettingsState);
                    break;
                case ButtonNames.CollectionContent:
                    ChangeCollectionContent();
                    break;
            }
        }

        private void ChangeCollectionContent()
        {
            ChangeCollection();

            if (!Model.ContainsKey(ButtonNames.CollectionContent))
            {
                Model.Set(ButtonNames.CollectionContent, _collection);
            }
            else
            {
                Model.Set(ButtonNames.CollectionContent, _collection).Refresh(ButtonNames.CollectionContent);
            }
        }

        private void ChangeCollection()
        {
            var countIterations = GetRandomValue();

            _collection.Clear();

            for (var i = 0; i < countIterations; i++)
            {
                _collection.Add(new CollectionData(Random.Range(0, 100), ButtonNames.CollectionContent, 0));
            }
        }

        private int GetRandomValue()
        {
            return Random.Range(_MIN_VALUE_FOR_RANDOM_RANGE, _MAX_VALUE_FOR_RANDOM_RANGE);
        }
    }
}

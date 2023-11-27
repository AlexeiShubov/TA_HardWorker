using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(StateNames.GameState)]
public class GameState : FSMState
{
    private const int _MIN_VALUE_FOR_RANDOM_RANGE = 1;
    private const int _MAX_VALUE_FOR_RANDOM_RANGE = 10;

    private readonly List<int> _collection = new List<int>();
    
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
        var randomValue = GetRandomValue();
        
        _collection.Clear();
        
        for (var i = 0; i < randomValue; i++)
        {
            _collection.Add(GetRandomValue());
        }
    }

    private int GetRandomValue()
    {
        return Random.Range(_MIN_VALUE_FOR_RANDOM_RANGE, _MAX_VALUE_FOR_RANDOM_RANGE);
    }
}

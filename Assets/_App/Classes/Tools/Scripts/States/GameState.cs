using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

[State(StateNames.GameState)]
public class GameState : AbstractState
{
    private const int _MIN_VALUE_FOR_RANDOM_RANGE = 1;
    private const int _MAX_VALUE_FOR_RANDOM_RANGE = 10;

    private readonly List<int> _collection = new List<int>();
    
    [Enter]
    private void Enter()
    {
        EnableSettingsToggles(false);

        Model.Set(ButtonNames.OnBtnGameEnableChanged, false);
        Model.Set(ButtonNames.OnBtnSettingsEnableChanged, true);
        Model.Set(ButtonNames.OnBtnCollectionContentChanged, true);
        
        Debug.Log($"Enter to {StateNames.GameState}");
    }

    [Bind(ButtonNames.OnBtn)]
    private void OnSomeButtonClick(string buttonName)
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

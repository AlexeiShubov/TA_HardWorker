using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public class FeelProjectManager : MonoBehaviourExtBind
{
    [SerializeField] private GameObject _settingsPanel;
    
    [Bind]
    private void OnToggleMusicClick()
    {
        Model.EventManager.Invoke(
            Model.GetBool(ToggleNames.OnToggleMusicClick) ? SoundNames.PlayMusic : SoundNames.StopMusic);
    }
    
    [Bind]
    private void SoundPlay(string soundName)
    {
        if (soundName == "Click" && Model.GetBool(ToggleNames.OnToggleSoundClick))
        {
            Model.EventManager.Invoke(SoundNames.PlayClick);
        }
    }
    
    [Bind]
    private void OnCollectionContentChanged()
    {
        Model.Set(ButtonNames.OnCollectionContentClick, string.Join(", ", Model.Get<List<int>>(ButtonNames.CollectionContent)));
    }

    [Bind]
    private void OnSettingsPanelActiveChanged(bool status)
    {
        _settingsPanel.SetActive(status);
    }
}

using System.Collections.Generic;
using AxGrid.Base;
using AxGrid.Model;

public class FeelProjectManager : MonoBehaviourExtBind
{
    [Bind(ToggleNames.OnToggleMusicClick)]
    private void OnToggleMusicClick()
    {
        Model.EventManager.Invoke(
            Model.GetBool(ToggleNames.OnToggleMusicClick) ? SoundNames.PlayMusic : SoundNames.StopMusic);
    }
    
    [Bind("SoundPlay")]
    private void OnToggleSoundClick(string soundName)
    {
        if (soundName == "Click" && Model.GetBool(ToggleNames.OnToggleSoundClick))
        {
            Model.EventManager.Invoke(SoundNames.PlayClick);
        }
    }
    
    [Bind(ButtonNames.OnCollectionContentChanged)]
    private void OnCollectionContentChanged()
    {
        var collection = Model.Get<List<int>>(ButtonNames.CollectionContent);

        var listValues = "";
        var countIterations = collection.Count;

        for (var i = 0; i < countIterations; i++)
        {
            var separator = i == 0 ? "" : ", ";

            listValues = $"{listValues}{separator}{collection[i]}";
        }

        Model.Set(ButtonNames.OnCollectionContentClick, listValues);
    }
}

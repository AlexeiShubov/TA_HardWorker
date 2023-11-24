using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public class AudioManager : MonoBehaviourExtBind
{
    [Bind(ToggleNames.OnToggleMusicClick)]
    private void ChangeMusicStatus()
    {
        var status = Model.Get<bool>(ToggleNames.OnToggleMusicClick);
        
        Debug.LogError($"Music status: {status}");
    }
    
    [Bind(ToggleNames.OnToggleSoundClick)]
    private void ChangeSoundStatus()
    {
        var status = Model.Get<bool>(ToggleNames.OnToggleSoundClick);
        
        Debug.LogError($"Sound status: {status}");
    }
    
    [Bind(ToggleNames.OnToggleVibrationClick)]
    private void ChangeVibrationStatus()
    {
        var status = Model.Get<bool>(ToggleNames.OnToggleVibrationClick);
        
        Debug.LogError($"Vibration status: {status}");
    }
}

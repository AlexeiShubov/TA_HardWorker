using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public class AudioManager : MonoBehaviourExtBind
{
    [Bind(ToggleNames.MusicToggleClick)]
    private void ChangeMusicStatus()
    {
        var status = Model.Get<bool>(ToggleNames.MusicToggleClick);
        
        Debug.LogError($"Music status: {status}");
    }
    
    [Bind(ToggleNames.SoundToggleClick)]
    private void ChangeSoundStatus()
    {
        var status = Model.Get<bool>(ToggleNames.SoundToggleClick);
        
        Debug.LogError($"Sound status: {status}");
    }
    
    [Bind(ToggleNames.VibrationToggleClick)]
    private void ChangeVibrationStatus()
    {
        var status = Model.Get<bool>(ToggleNames.VibrationToggleClick);
        
        Debug.LogError($"Vibration status: {status}");
    }
}

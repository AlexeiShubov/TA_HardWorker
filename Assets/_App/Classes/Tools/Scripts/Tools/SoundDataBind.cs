using AxGrid.Base;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundDataBind : MonoBehaviourExtBind
{
    [Header("Имя события")]
    [SerializeField] private string _fieldName;
    
    private AudioSource _audioSource;
    
    [OnAwake]
    private void CustomAwake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    [OnStart]
    private void CustomStart()
    {
        Model.EventManager.AddAction($"Play{_fieldName}", PlaySound);
        Model.EventManager.AddAction($"Stop{_fieldName}", StopSound);
    }

    [OnDestroy]
    private void CustomDestroy()
    {
        Model.EventManager.RemoveAction($"Play{_fieldName}", PlaySound);
        Model.EventManager.RemoveAction($"Stop{_fieldName}", StopSound);
    }

    private void PlaySound()
    {
        _audioSource.Play();
    }

    private void StopSound()
    {
        _audioSource.Stop();
    }
}

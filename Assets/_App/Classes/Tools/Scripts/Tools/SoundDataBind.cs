using AxGrid.Base;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundDataBind : MonoBehaviourExtBind
{
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private bool _playOnAwake;
    [SerializeField] private bool _loop;
    [Range(0f, 1f)]
    [SerializeField] private float _volume;
    [Space(5)]
    [Header("Имя события")]
    [SerializeField] private string _fieldName;
    
    private AudioSource _audioSource;
    
    [OnAwake]
    private void CustomAwake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _audioClip;
        _audioSource.playOnAwake = _playOnAwake;
        _audioSource.loop = _loop;
        _audioSource.volume = _volume;

        if (_playOnAwake)
        {
            PlaySound();
        }
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

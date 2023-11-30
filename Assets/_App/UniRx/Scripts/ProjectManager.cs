using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ProjectManager : MonoBehaviour, IRestartable
{
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private ChangerProgress _changerProgress;
    [SerializeField] private Button _restartButton;

    private CompositeDisposable _disposable;
    private List<IRestartable> _restartables;
    
    private void Awake()
    {
        _restartButton.interactable = false;
        
        _restartables = new List<IRestartable>
        {
            _sceneLoader,
            _changerProgress,
        };
        
        _changerProgress.Initialize();
        _sceneLoader.Initialize(_changerProgress);
        
        Subscribe();
    }

    public void Restart()
    {
        _disposable.Clear();
        _restartButton.interactable = false;

        foreach (var restartable in _restartables)
        {
            restartable.Restart();
        }
        
        Subscribe();
    }

    private void OnErrorLoadingScene()
    {
        _restartButton.interactable = true;
        _disposable.Clear();
        Debug.LogError("Custom Error Loading Scene");
    }

    private void Subscribe()
    {
        _disposable = new CompositeDisposable();
        
        _restartButton
            .OnClickAsObservable()
            .Subscribe(_ =>
        {
            Restart();
        });

        _changerProgress.LoadingError
            .Where(value => value)
            .Subscribe(_ =>
        {
            OnErrorLoadingScene();
        });
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}

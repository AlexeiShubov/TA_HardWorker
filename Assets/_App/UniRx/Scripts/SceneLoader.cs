using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IRestartable
{
    private ChangerProgress _changerProgress;
    private CompositeDisposable _disposable;

    public void Initialize(ChangerProgress changerProgress)
    {
        _changerProgress = changerProgress;
        _disposable = new CompositeDisposable();
        
        Subscribe();
    }

    public void Restart()
    {
        _disposable.Clear();

        Subscribe();
    }

    private void Subscribe()
    {
        _disposable = new CompositeDisposable();
        
        _changerProgress.LoadingProgress
            .Where(t => t >= 1f)
            .Subscribe(value =>
            {
                SceneManager.LoadScene(1);
            }).AddTo(_disposable);

        _changerProgress.LoadingError
            .Where(value => value)
            .Subscribe(_ =>
        {
            _disposable.Clear();
        });
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}

public interface IRestartable
{
    public abstract void Restart();
}

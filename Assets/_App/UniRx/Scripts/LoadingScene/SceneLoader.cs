using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UniRxTask
{
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

            _changerProgress.loadingProgress
                .Where(t => t >= 1f)
                .Subscribe(value => { SceneManager.LoadScene(1); }).AddTo(_disposable);

            _changerProgress.exception
                .Subscribe(_ =>
                {
                    Debug.LogError("Custom Error Loading Scene");
                    _disposable.Clear();
                }).AddTo(_disposable);
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
}
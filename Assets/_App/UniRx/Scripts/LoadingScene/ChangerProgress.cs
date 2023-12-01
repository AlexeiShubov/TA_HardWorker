using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UniRxTask
{
    public class ChangerProgress : MonoBehaviour, IRestartable
    {
        [SerializeField] private Image _progressImage;
        [SerializeField] private int _countIterations;
        [Range(0f, 1f)] [SerializeField] private float _errorChance = 0.5f;

        private CompositeDisposable _disposable;
        private ReactiveCommand<float> _createException;
        private float _counter;

        public Subject<Exception> exception;
        public ReactiveProperty<float> loadingProgress;

        public void Initialize()
        {
            exception = new Subject<Exception>();
            loadingProgress = new ReactiveProperty<float>();

            Subscribe();
            DoAction();
        }

        public void Restart()
        {
            _disposable.Clear();

            _counter = default;
            loadingProgress.Value = default;
            _progressImage.fillAmount = default;

            Subscribe();
            DoAction();
        }

        private void Subscribe()
        {
            _disposable = new CompositeDisposable();
            _createException = new ReactiveCommand<float>();

            loadingProgress
                .Where(value => value >= 0.9f)
                .First()
                .Subscribe(_ => { _createException.Execute(Random.value); }).AddTo(_disposable);

            _createException
                .Subscribe(CreateException)
                .AddTo(_disposable);
        }

        private void DoAction()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    loadingProgress.Value = ++_counter / _countIterations;
                    _progressImage.fillAmount = loadingProgress.Value;
                }).AddTo(_disposable);
        }

        private void CreateException(float value)
        {
            if (value <= _errorChance)
            {
                exception.OnNext(new Exception("Scene loading error!"));
                _disposable.Clear();
            }
        }

        private void OnDisable()
        {
            _disposable.Clear();
        }
    }
}
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ChangerProgress : MonoBehaviour, IRestartable
{
    [SerializeField] private Image _progressImage;
    [SerializeField] private int _countIterations;
    [Range(0f, 1f)]
    [SerializeField] private float _errorChance = 0.5f;
    
    private CompositeDisposable _disposable;
    private ReactiveCommand<float> _createException;
    private float _counter;
    
    public ReactiveProperty<float> LoadingProgress;
    public ReactiveProperty<bool> LoadingError;

    public void Initialize()
    {
        LoadingProgress = new ReactiveProperty<float>();
        LoadingError = new ReactiveProperty<bool>();
        
        Subscribe();
        DoAction();
    }

    public void Restart()
    {
        _disposable.Clear();
        
        _counter = default;
        LoadingProgress.Value = default;
        LoadingError.Value = default;
        _progressImage.fillAmount = default;
        
        Subscribe();
        DoAction();
    }

    private void Subscribe()
    {
        _disposable = new CompositeDisposable();
        _createException = new ReactiveCommand<float>();

        LoadingProgress
            .Where(value => value >= 0.9f)
            .Subscribe(_ =>
            {
                _createException.Execute(Random.value);
            }).AddTo(_disposable);

        _createException
            .Subscribe(CreateException)
            .AddTo(_disposable);
    }

    private void DoAction()
    {
        Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                LoadingProgress.Value = ++_counter / _countIterations;
                _progressImage.fillAmount = LoadingProgress.Value;
            }).AddTo(_disposable);
    }

    private void CreateException(float value)
    {
        if (value <= _errorChance)
        {
            LoadingError.Value = true;
            _disposable.Clear();
        }
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}

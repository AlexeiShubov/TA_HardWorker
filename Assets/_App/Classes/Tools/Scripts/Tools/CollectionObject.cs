using AxGrid.Path;
using STIGRADOR;
using TMPro;
using UnityEngine;

namespace ClassesTools
{
    public class CollectionObject : BasePoolObject
    {
        [SerializeField] private float _moveDelay = 0.5f;
        [SerializeField] protected TextMeshProUGUI _text;

        private CPath _cPath;
        private CollectionData _data;
        private SpriteRenderer _spriteRenderer;
        private Canvas _canvas;

        private bool _isMoving;

        public CollectionData Data => _data;

        public void Init(CollectionData data)
        {
            gameObject.SetActive(true);
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _canvas = GetComponentInChildren<Canvas>();
            
            UpdateData(data);
        }

        private void Update()
        {
            if(!_isMoving) return;
            
            _cPath?.Update(Time.deltaTime);
        }

        public override void Return()
        {
            base.Return();
            gameObject.SetActive(false);
        }

        public void MoveTo(Vector2 targetPosition)
        {
            _isMoving = true;
            _cPath?.Clear();
            _cPath = CPath.Create();

            var startLocalPosition = transform.localPosition;

            _cPath.EasingQuadEaseOut(_moveDelay, 0f, 1f, t =>
            {
                transform.localPosition = Vector2.Lerp(startLocalPosition, targetPosition, t);
            }).Action(()=>_isMoving = false);
        }

        private void UpdateData(CollectionData data)
        {
            _data = data;
            _text.text = _data.info.ToString();
            _spriteRenderer.sortingOrder = data.Priority;
            _canvas.sortingOrder = data.Priority;
            
            Debug.LogError(_data);
        }
    }
}

using STIGRADOR;
using TMPro;
using UnityEngine;

namespace ClassesTools
{
    public class CollectionObject : BasePoolObject
    {
        [SerializeField] protected TextMeshProUGUI _text;

        private CollectionData _data;
        private SpriteRenderer _spriteRenderer;
        private Canvas _canvas;

        public CollectionData Data => _data;

        public void Init(CollectionData data)
        {
            gameObject.SetActive(true);
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _canvas = GetComponentInChildren<Canvas>();

            _data = data;
            _text.text = _data.info.ToString();
            _spriteRenderer.sortingOrder = data.Priority;
            _canvas.sortingOrder = data.Priority;
        }

        public override void Return()
        {
            base.Return();
            gameObject.SetActive(false);
        }
    }
}

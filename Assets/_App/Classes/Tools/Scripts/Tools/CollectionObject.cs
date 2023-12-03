using STIGRADOR;
using TMPro;
using UnityEngine;

namespace ClassesTools
{
    public class CollectionObject : BasePoolObject
    {
        [SerializeField] protected TextMeshProUGUI _text;
        
        protected CollectionData _data;
        protected SpriteRenderer _spriteRenderer;
        protected Canvas _canvas;

        public CollectionData Data => _data;

        public void Init(CollectionData data, int index)
        {
            gameObject.SetActive(true);
            _data = data;
            _text.text = _data.info.ToString();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _canvas = GetComponentInChildren<Canvas>();

            _spriteRenderer.sortingOrder = index;
            _canvas.sortingOrder = index;
        }

        public override void Return()
        {
            base.Return();

            gameObject.SetActive(false);
        }
    }
}

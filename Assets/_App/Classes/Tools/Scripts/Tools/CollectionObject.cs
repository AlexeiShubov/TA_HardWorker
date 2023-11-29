using STIGRADOR;
using TMPro;
using UnityEngine;

namespace ClassesTools
{
    public class CollectionObject : BasePoolObject
    {
        [SerializeField] protected TextMeshProUGUI _text;
        
        protected CollectionData _data;

        public CollectionData Data => _data;

        public void Init(CollectionData data)
        {
            gameObject.SetActive(true);
            _data = data;
            _text.text = _data.info;
        }

        public override void Return()
        {
            base.Return();

            gameObject.SetActive(false);
        }
    }
}

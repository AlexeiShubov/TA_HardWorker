using AxGrid;
using ClassesTools;
using UnityEngine.EventSystems;

namespace Task2
{
    public class CollectionObjectTaskTwo : CollectionObject, IPointerClickHandler
    {
        private const string _ON_COLLECTION_OBJECT_CLICK = "OnCollectionObjectClick";

        public void OnPointerClick(PointerEventData eventData)
        {
            Settings.Invoke(_ON_COLLECTION_OBJECT_CLICK, this);
        }

        public void OnClick()
        {
            OnPointerClick(null);
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace _App._Slot
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private int _ID;
        [SerializeField] private Sprite _idleSprite;
        [SerializeField] private Sprite _movingSprite;
        [SerializeField] private Image _currentImage;

        public int ID => _ID;

        public void Init()
        {
            _currentImage.sprite = _idleSprite;
        }
    }
}

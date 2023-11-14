using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image)), RequireComponent(typeof(PathMoveBlock))]
public class Block : MonoBehaviour
{
    [SerializeField] private int _ID;
    [SerializeField] private Sprite _idleSprite;
    [SerializeField] private Sprite _movingSprite;
    [SerializeField] private Image _currentImage;
    
    private PathMoveBlock _pathMoveBlock;
    private Transform _targetPosition;

    public int ID => _ID;

    public void Init(Transform targetPosition, Transform defaultPosition)
    {
        _currentImage = GetComponent<Image>();
        _pathMoveBlock = GetComponent<PathMoveBlock>();
        
        _targetPosition = targetPosition;
        _currentImage.sprite = _idleSprite;
        _pathMoveBlock.Init(this, _targetPosition.localPosition, defaultPosition.localPosition);
    }
}

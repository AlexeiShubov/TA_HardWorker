using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;

public sealed class PathMoveBlock : MonoBehaviourExt
{
    private Vector2 _targetPosition;
    private Vector2 _defaultPosition;
    private Transform _transform;

    public void Init(Block block, Vector2 targetPosition, Vector2 defaultPosition)
    {
        _targetPosition = targetPosition + new Vector2(0f, block.transform.localScale.y - 161f);
        _defaultPosition = defaultPosition;
        _transform = block.GetComponent<Transform>();

        Path = new CPath();
        MoveBlock();
    }

    public void MoveBlock()
    {
        var startPosition = _transform.localPosition;
        var delay = ((Vector2)_transform.localPosition - _targetPosition) / 3f;
        
        Path.EasingLinear(delay.y / 100, 0, 1, f =>
            {
                _transform.localPosition = Vector2.Lerp(startPosition, _targetPosition, f);
            })
            .Action(() =>
            {
                _transform.localPosition = _defaultPosition;
                MoveBlock();
            });
    }
}

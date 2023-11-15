using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PathMoveBlock)), RequireComponent(typeof(PathStoppingBlock))]
public class Slot : MonoBehaviour
{
    private const float MOVE_SPEED_BLOCKS = 3000f;

    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private Block[] _blocks;
    
    private bool _isMoving;

    private Vector2 _targetPosition;
    private Vector2 _topPosition;
    private PathMoveBlock _pathMoveBlock;
    private PathStoppingBlock _pathStoppingBlock;

    public void Init()
    {
        _pathMoveBlock = GetComponent<PathMoveBlock>();
        _pathStoppingBlock = GetComponent<PathStoppingBlock>();
        _targetPosition = GetTargetPosition();
        _topPosition = GetTopPosition();

        _pathMoveBlock.Init(_blocks, MOVE_SPEED_BLOCKS, _targetPosition);
        _pathStoppingBlock.Init(_blocks, MOVE_SPEED_BLOCKS, _targetPosition, _topPosition);
        _gridLayoutGroup.enabled = true;
        BlockInitialization();
    }
    
    public void DoAction()
    {
        ChangeSpritesToBlocks(true);
        _pathMoveBlock.MoveBlocks();
    }

    public void StopSlot(int winningBlockID)
    {
        ChangeSpritesToBlocks(false);
        _pathMoveBlock.StopBlocks();
        _pathStoppingBlock.StoppingBlocks(winningBlockID);
    }

    private Vector2 GetTargetPosition()
    {
        var result = new Vector2(
                _blocks[0].transform.localPosition.x, 
                _blocks[^1].transform.localPosition.y - 
                (_blocks[0].transform.localPosition.y - _blocks[1].transform.localPosition.y) 
                - _blocks[0].transform.localScale.y);

        return result;
    }

    private Vector2 GetTopPosition()
    {
        var result = new Vector2(_blocks[0].transform.localPosition.x, _blocks[1].transform.localPosition.y + (_blocks[0].transform.localPosition.y - _blocks[1].transform.localPosition.y) + _blocks[0].transform.localScale.y);

        return result;
    }
    
    private void BlockInitialization()
    {
        foreach (var block in _blocks)
        {
            block.Init();
        }
    }

    private void ChangeSpritesToBlocks(bool movingStatus)
    {
        foreach (var block in _blocks)
        {
            block.GetComponent<Image>().color = Color.white;
            block.ChangeSprite(movingStatus);
        }
    }
}

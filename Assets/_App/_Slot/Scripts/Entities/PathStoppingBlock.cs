using System.Collections.Generic;
using System.Linq;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.UI;

public class PathStoppingBlock : MonoBehaviourExt
{
    private const float MOVE_SPEED_BLOCKS = 2000f;
    private const float _ADDITIONAL_TIME_DELAY = 3f;
    private const float _MIDLE_OFFSET = 40f;
    private const float _SPRING_SPEED_OFFSET = 0.5f;

    private Block[] _blocks;
    private Vector3 _midlePosition;
    private Vector3 _offsetPosition;
    private Vector3 _targetPosition;
    private Vector3 _topPosition;
    private List<Transform> _blocksTransform;
    private GridLayoutGroup _gridLayoutGroup;

    public void Init(Block[] blocks, Vector2 targetPosition, Vector2 topPosition, GridLayoutGroup gridLayoutGroup)
    {
        _blocks = blocks;
        _blocksTransform = new List<Transform>();
        _gridLayoutGroup = gridLayoutGroup;
        
        foreach (var block in blocks)
        {
            _blocksTransform.Add(block.transform);
        }

        _midlePosition = _blocksTransform[^2].localPosition;
        _offsetPosition = _midlePosition - new Vector3(0f, _MIDLE_OFFSET);

        _targetPosition = targetPosition;
        _topPosition = topPosition;
    }
    
    public void StoppingBlocks(int winningID)
    {
        CreatePath();
        var winningBlock = _blocks.First(t => t.ID == winningID);
        var winningTransform = winningBlock.transform;

        Path
            .EasingLinear(_ADDITIONAL_TIME_DELAY, 0, 1, f =>
            {
                foreach (var blockTransform in _blocksTransform)
                {
                    if (blockTransform.localPosition.y <= _targetPosition.y)
                    {
                        blockTransform.SetAsFirstSibling();
                    }

                    blockTransform.localPosition = new Vector3(0f, blockTransform.localPosition.y - MOVE_SPEED_BLOCKS * Time.deltaTime);
                }
                
                if (winningTransform.localPosition.y <= _offsetPosition.y)
                {
                    Spring(winningBlock);
                }
            });
    }

    private void Spring(Block winningBlock)
    {
        CreatePath();

        Path
            .EasingLinear(_ADDITIONAL_TIME_DELAY, 0, 1, f =>
            {
                foreach (var blockTransform in _blocksTransform)
                {
                    if (blockTransform.localPosition.y >= _topPosition.y)
                    {
                        blockTransform.SetAsLastSibling();
                    }
                    
                    blockTransform.localPosition = new Vector3(0f, blockTransform.localPosition.y + MOVE_SPEED_BLOCKS * _SPRING_SPEED_OFFSET * Time.deltaTime);
                }
                
                if (winningBlock.transform.localPosition.y >= _midlePosition.y)
                {
                    _gridLayoutGroup.enabled = false;
                    _gridLayoutGroup.enabled = true;
                    winningBlock.ActiveFrameEffect(true);
                    Model.EventManager.Invoke(Keys.AllBlocksIsIdle);
                    Path = null;
                }
            });
    }

    private void CreatePath()
    {
        Path = null;
        Path = CPath.Create();
    }
}

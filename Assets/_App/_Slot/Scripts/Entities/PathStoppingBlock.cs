using System.Collections.Generic;
using System.Linq;
using AxGrid.Base;
using AxGrid.Path;
using UnityEngine;
using UnityEngine.UI;

public class PathStoppingBlock : MonoBehaviourExt
{
    private const float _ADDITIONAL_TIME_DELAY = 2f;
    private const float _MIDLE_OFFSET = 80f;
    private const float _SPRING_SPEED_OFFSET = 0.25f;

    private Block[] _blocks;
    private Vector3 _midlePosition;
    private Vector3 _offsetPosition;
    private Vector3 _targetPosition;
    private Vector3 _topPosition;
    private List<Transform> _blocksTransform;
    
    private float _movingSpeed;

    public void Init(Block[] blocks, float moveSpeed, Vector2 targetPosition, Vector2 topPosition)
    {
        _blocks = blocks;
        _blocksTransform = new List<Transform>();
        
        foreach (var block in blocks)
        {
            _blocksTransform.Add(block.transform);
        }

        _movingSpeed = moveSpeed;
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

                    blockTransform.localPosition = new Vector3(0f, blockTransform.localPosition.y - _movingSpeed * Time.deltaTime);
                }
                
                if (winningTransform.localPosition.y <= _offsetPosition.y)
                {
                    Debug.LogWarning(winningBlock.transform.localPosition.y + " " + _offsetPosition.y + " " + winningBlock);
                    winningBlock.GetComponent<Image>().color = Color.black;

                    Spring(winningBlock.transform);
                }
            });
    }

    private void Spring(Transform winningBlock)
    {
        CreatePath();

        Path
            .Wait(0.05f)
            .EasingLinear(_ADDITIONAL_TIME_DELAY, 0, 1, f =>
            {
                foreach (var blockTransform in _blocksTransform)
                {
                    if (blockTransform.localPosition.y >= _topPosition.y)
                    {
                        blockTransform.SetAsLastSibling();
                    }
                    
                    blockTransform.localPosition = new Vector3(0f, blockTransform.localPosition.y + _movingSpeed * _SPRING_SPEED_OFFSET * Time.deltaTime);
                }
                
                if (winningBlock.localPosition.y >= _midlePosition.y)
                {
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

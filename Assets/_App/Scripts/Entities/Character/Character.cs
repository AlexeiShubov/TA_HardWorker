using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform _body;
    
    private IMovable _iMovable;
    private IFlipable _iFlipable;

    public void Init(IMovable iMovable, IFlipable iFlipable)
    {
        _iMovable = iMovable;
        _iFlipable = iFlipable;
    }

    public void Move(Transform targetPosition, Action callback = null)
    {
        _iFlipable.Flip(targetPosition.position, _body);
        _iMovable.Move(transform, targetPosition, callback);
    }
}

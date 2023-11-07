using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    private IMovable _iMovable;

    public void Init(IMovable iMovable)
    {
        _iMovable = iMovable;
    }

    public void Move(Transform targetPosition, Action callback = null)
    {
        _iMovable.Move(transform, targetPosition, callback);
    }
}

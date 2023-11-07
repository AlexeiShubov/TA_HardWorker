using System;
using UnityEngine;

public interface IMovable
{
    public abstract void Move(Transform transform, Transform targetPosition, Action callback = null);
}

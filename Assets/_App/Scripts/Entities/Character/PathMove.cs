using System;
using AxGrid;
using AxGrid.Path;
using UnityEngine;

public class PathMove : IMovable
{
    private readonly SOGlobalSettings _soGlobalSettings;
    private readonly CPath _path;
    
    public PathMove(CPath path, SOGlobalSettings soGlobalSettings)
    {
        _path = path;
        _soGlobalSettings = soGlobalSettings;
    }
    
    public void Move(Transform transform, Transform targetPosition, Action callback = null)
    {
        var startPosition = transform.position;

        _path
            .EasingLinear(_soGlobalSettings.DelayAnimationCharacterMove, 0, 1, f =>
            {
                transform.position = Vector2.Lerp(startPosition, targetPosition.position, f);
            })
            .Action(() =>
            {
                Settings.Fsm.Invoke(NamesEvent.FinishPath);
                callback?.Invoke();
            });
    }
}

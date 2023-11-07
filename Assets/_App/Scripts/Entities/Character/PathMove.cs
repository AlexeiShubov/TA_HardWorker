using System;
using AxGrid;
using AxGrid.Path;
using UnityEngine;

public class PathMove : IMovable
{
    private readonly SOGlobalSettings _soGlobalSettings;
    
    private CPath _path;
    
    public PathMove(CPath path, SOGlobalSettings soGlobalSettings)
    {
        _path = path;
        _soGlobalSettings = soGlobalSettings;
    }
    
    public void Move(Transform transform, Transform targetPosition, Action callback = null)
    {
        //_path.StopPath();

        /*var startPosition = transform.position;
        var delay = (startPosition - targetPosition.position).sqrMagnitude / _soGlobalSettings.CharacterMoveSpeed;*/

        var delay = 2f;
        var startPosition = transform.position;

        _path
            .EasingLinear(delay, 0, 1, f =>
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

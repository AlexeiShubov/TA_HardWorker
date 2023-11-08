using UnityEngine;

public class FlipByX : IFlipable
{
    public void Flip(Vector3 targetPosition, Transform body)
    {
        var localScale = body.localScale;
        var direction = targetPosition.x - body.position.x;

        if (direction <= 0 && localScale.x > 0 || direction > 0 && localScale.x < 0)
        {
            localScale = new Vector2(-localScale.x, localScale.y);
        }

        body.localScale = localScale;
    }
}

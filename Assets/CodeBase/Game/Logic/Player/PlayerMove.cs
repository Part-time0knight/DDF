using UnityEngine;

public class PlayerMove
{

    public bool Stop { get; set; }

    private const float SPEED = 6;

    private readonly Transform _transform;

    public PlayerMove(Transform transform)
    {
        _transform = transform;
    }

    public void MoveHorizontal(float speedMultiplier)
    {
        if (Stop)
            return;
        _transform.localPosition += new Vector3(
            speedMultiplier * SPEED * Time.fixedDeltaTime, 0f);
    }

    public void MoveVertical(float speedMultiplier)
    {
        if (Stop)
            return;
        _transform.localPosition += new Vector3(0f,
            speedMultiplier * SPEED * Time.fixedDeltaTime);
    }
}

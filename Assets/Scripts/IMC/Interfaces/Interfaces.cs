using UnityEngine;
public interface IMovement
{
    void Move(Vector2 input);
}

public interface IRotate
{
    void Rotate(Vector2 input);
}

public interface IFire
{
    void Fire();
}

public interface IMovementLocked
{
    void ToggleLock();
    bool IsLocked { get; }
}
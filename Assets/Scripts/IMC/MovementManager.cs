using UnityEngine;
public class MovementController : IMovement, IRotate, IMovementLocked
{
    private readonly Rigidbody rb;
    private readonly float speed;
    private readonly float rotationSpeed;
    private bool isLocked;

    public bool IsLocked => isLocked;

    public MovementController(Rigidbody rb, float speed, float rotationSpeed)
    {
        this.rb = rb;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
    }

    public void Move(Vector2 input)
    {
        if (isLocked) return;

        Vector3 direction = new Vector3(input.x, 0, input.y);
        Vector3 movement = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    public void Rotate(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, 0, input.y);
        if (direction.sqrMagnitude < 0.01f) return;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        Quaternion smoothedRot = Quaternion.RotateTowards(
            rb.rotation,
            targetRot,
            rotationSpeed * Time.fixedDeltaTime
        );
        rb.MoveRotation(smoothedRot);
    }

    public void ToggleLock()
    {
        isLocked = !isLocked;
        Debug.Log("Movement Locked: " + isLocked);
    }
}

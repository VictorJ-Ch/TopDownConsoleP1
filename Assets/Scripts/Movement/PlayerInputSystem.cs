using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour, IA_Player.IPlayerActions
{
    private IA_Player inputActions;

    private IMovement playerMovement;
    private IRotate playerRot;
    private IFire fireManager;
    private IMovementLocked movementLocked;

    private Vector2 moveInput;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotSpeed = 720f;

    [Header("Fire Settings")]
    [SerializeField] private Transform fireSpawn;
    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        var rb = GetComponent<Rigidbody>();

        playerMovement = new MovementController(rb, speed, rotSpeed);
        playerRot = (IRotate)playerMovement;
        movementLocked = (IMovementLocked)playerMovement;

        fireManager = new FireController(fireSpawn, bulletPrefab, 10);

        inputActions = new IA_Player();
        inputActions.Player.SetCallbacks(this);
        inputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        playerRot.Rotate(moveInput);
        playerMovement.Move(moveInput);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLockMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            movementLocked.ToggleLock();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            fireManager.Fire();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;

    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
    }

    private void OnEnable()
    {
        _gameInput.Player.Flashlight.performed += OnFlashlightToggled;
        _gameInput.Enable();
    }

    private void OnDisable()
    {
        _gameInput.Player.Flashlight.performed -= OnFlashlightToggled;
        _gameInput.Disable();
    }

    private void Update()
    {
        Vector3 movementInput = _gameInput.Player.Move.ReadValue<Vector3>();
        Vector2 lookInput = _gameInput.Player.Look.ReadValue<Vector2>();
        _playerMover.SetMovementDirection(movementInput);
        _playerMover.Rotate(lookInput);
    }

    private void OnFlashlightToggled(InputAction.CallbackContext callbackContext)
    {
        _player.ToggleFlashlight();
    }
}

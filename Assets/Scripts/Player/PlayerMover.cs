using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField, Min(0)] private float _movementSpeed = 1;
    [SerializeField, Min(0)] private float _rotationSpeed = 1;
    [SerializeField, Range(0, 89)] private float _headInclinationLimit = 89;
    [SerializeField] private Transform _head;

    private Vector3 _movementDirection;
    private float _headInclination;
    private Vector3 _fallSpeed;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 movementSpeed;

        if (_characterController.isGrounded)
        {
            _fallSpeed = Physics.gravity;
            movementSpeed = transform.TransformDirection(_movementSpeed * _movementDirection);
        }
        else
        {
            _fallSpeed += Time.deltaTime * Physics.gravity;
            movementSpeed = _characterController.velocity;
            movementSpeed.y = 0;
        }

        movementSpeed += _fallSpeed;
        _characterController.Move(Time.deltaTime * movementSpeed);
    }

    public void SetMovementDirection(Vector3 movementDirection)
    {
        movementDirection.y = 0;
        _movementDirection = movementDirection;
    }

    public void Rotate(Vector2 lookRotation)
    {
        transform.Rotate(0, _rotationSpeed * lookRotation.x, 0);

        float verticalRotation = _rotationSpeed * lookRotation.y;
        _headInclination = Mathf.Clamp(_headInclination + verticalRotation, -_headInclinationLimit, _headInclinationLimit);
        _head.localRotation = Quaternion.Euler(_headInclination, 0, 0);
    }
}

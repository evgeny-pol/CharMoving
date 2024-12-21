using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField, Min(0)] private float _movementSpeed = 1;
    [Tooltip("На каком расстоянии от цели нужно остановиться.")]
    [SerializeField, Min(0)] private float _approachDistance = 1;
    [Tooltip("На каком расстоянии от целевой позиции нужно начать замедлять движение.")]
    [SerializeField, Min(0.01f)] private float _approachSlowdownDistance = 1;
    [SerializeField, Min(0)] private float _rotationSpeed = 90;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float rotationSpeed = _rotationSpeed * Time.fixedDeltaTime;
        Vector3 currentPosition = _rigidbody.position;
        Vector3 targetPosition = _followTarget.position;
        targetPosition.y = currentPosition.y;
        Vector3 toTargetPosition = targetPosition - currentPosition;
        Quaternion currentRotation = _rigidbody.rotation;
        Quaternion toTargetRotation = Quaternion.LookRotation(toTargetPosition);
        Quaternion newRotation = Quaternion.RotateTowards(currentRotation, toTargetRotation, rotationSpeed);
        _rigidbody.MoveRotation(newRotation);

        Vector3 toDesiredPosition = toTargetPosition + -toTargetPosition.normalized * _approachDistance;
        float movementSpeed = Mathf.Lerp(0, _movementSpeed, toDesiredPosition.magnitude / _approachSlowdownDistance);
        Vector3 forward = newRotation * Vector3.forward;
        Vector3 velocity = movementSpeed * Vector3.Dot(forward, toDesiredPosition.normalized) * forward;
        velocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = velocity;
    }
}

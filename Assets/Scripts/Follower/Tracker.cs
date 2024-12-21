using UnityEngine;

public class Tracker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector2 _rotationLimits;
    [SerializeField] private Vector2 _rotationSpeed;

    private Vector3 _rotation;

    private void Update()
    {
        Vector3 targetPositionLocal = transform.InverseTransformPoint(_target.position);
        Quaternion toTargetRotation = Quaternion.FromToRotation(Vector3.forward, targetPositionLocal);
        Vector3 toTargetRotationEuler = toTargetRotation.eulerAngles;
        _rotation.x = RotateAxis(_rotation.x, toTargetRotationEuler.x, _rotationSpeed.x, _rotationLimits.x);
        _rotation.y = RotateAxis(_rotation.y, toTargetRotationEuler.y, _rotationSpeed.y, _rotationLimits.y);
        transform.localRotation = Quaternion.Euler(_rotation);
    }

    private float RotateAxis(float currentRotation, float desiredDiff, float speed, float limit)
    {
        if (desiredDiff > MathConst.HalfCircleDegrees)
            desiredDiff -= MathConst.CircleDegrees;

        float angleDiff = Mathf.Min(speed * Time.deltaTime, Mathf.Abs(desiredDiff));
        return Mathf.Clamp(currentRotation + angleDiff * Mathf.Sign(desiredDiff), -limit, limit);
    }
}

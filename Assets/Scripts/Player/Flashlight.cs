using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private Light _light;

    private void Start()
    {
        _light.enabled = _isActive;
    }

    public void Toggle()
    {
        _isActive = !_isActive;
        _light.enabled = _isActive;
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Flashlight _flashlight;

    public void ToggleFlashlight()
    {
        _flashlight.Toggle();
    }
}

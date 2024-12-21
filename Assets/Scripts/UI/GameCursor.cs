using UnityEngine;

public class GameCursor : MonoBehaviour
{
    [SerializeField] private bool _isVisible = false;
    [SerializeField] private CursorLockMode _lockMode = CursorLockMode.Locked;

    private void Start()
    {
        Cursor.visible = _isVisible;
        Cursor.lockState = _lockMode;
    }
}

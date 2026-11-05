using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    public float distance = 5f;
    private float height = 2f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        rotationY += mouseDelta.x * mouseSensitivity;
        rotationX += mouseDelta.y * mouseSensitivity;

        rotationX = Mathf.Clamp(rotationX, -30f, 60f);
    }
    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0f);
        Vector3 offset = rotation * new Vector3(0f, height, -distance);
        transform.position = player.position + offset;
        transform.LookAt(player.position + Vector3.up * height);
    }
}

using UnityEngine;
using UnityEngine.Rendering;

public class DoorInteractable : Interactable
{
    public enum MoveDirection
    {
        Left,
        Right,
        Forward,
        Backward
    }
    public MoveDirection direction = MoveDirection.Right;
    public float openDistance = 2f;
    public float moveSpeed = 2f;
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen = false;

    private void Start()
    {
        closedPosition = transform.position;
        Vector3 movementDirection = GetMovementDirection();
        openPosition = closedPosition + movementDirection * openDistance;
    }
    public override void Interact()
    {
        Debug.Log("Door Interact Funciona");
        isOpen = !isOpen;
    }
    void Update()
    {
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
    Vector3 GetMovementDirection()
    {
        switch (direction)
        {
            case MoveDirection.Left: return Vector3.left;
            case MoveDirection.Right: return Vector3.right;
            case MoveDirection.Forward: return Vector3.forward;
            case MoveDirection.Backward: return Vector3.back;
            default: return Vector3.right;
        }
    }
}


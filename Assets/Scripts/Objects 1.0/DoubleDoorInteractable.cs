using UnityEngine;

public class DoubleDoorInteractable : Interactable
{
    public Transform leftDoor;
    public Transform rightDoor;

    public float openDistance = 2f;
    public float moveSpeed = 2f;

    private Vector3 leftClosedPosition;
    private Vector3 rightClosedPosition;

    private Vector3 leftOpenPosition;
    private Vector3 rightOpenPosition;

    private bool isOpen = false;

    private void Start()
    {
        leftClosedPosition = leftDoor.position;
        rightClosedPosition = rightDoor.position;
        leftOpenPosition = leftDoor.position + Vector3.left * openDistance;
        rightOpenPosition = rightClosedPosition + Vector3.right * openDistance;
    }

    public override void Interact()
    {
        isOpen = !isOpen;
    }
    private void Update()
    {
        Vector3 leftTarget = isOpen ? leftOpenPosition : leftClosedPosition;
        Vector3 rightTarget = isOpen ? rightOpenPosition : rightClosedPosition;
        leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTarget, moveSpeed*Time.deltaTime);
        rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTarget, moveSpeed*Time.deltaTime);
    }
}
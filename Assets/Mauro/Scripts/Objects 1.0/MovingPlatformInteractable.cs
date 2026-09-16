using UnityEngine;

public class MovingPlatformInteractable : Interactable
{
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private bool isMovingUp = false;

    void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + Vector3.up * moveDistance;
    }
    public override void Interact()
    {
        isMovingUp = !isMovingUp;
    }
    
    public void ActivatePlatform()
    {
        isMovingUp = !isMovingUp;
    }
    private void Update()
    {
        Vector3 targetPosition = isMovingUp ? endPosition : startPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

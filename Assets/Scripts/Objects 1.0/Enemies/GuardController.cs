
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardController : MonoBehaviour
{
    public enum GuardState
    {
        Patrol,
        Waiting,
        Chase
    }

    [Header("Patrulla")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float arrivalDistance = 0.5f;
    public float waitTime = 3f;

    private int currentPoint = 0;
    private float waitTimer = 0f;

    [Header("Persecucion")]
    public Transform player;
    public float chaseSpeed = 4f;
    public float detectionRange = 10f;
    public Transform eyePoint;

    [Header("Sonidos")]
    public AudioSource movementAudio;
    public AudioClip walkingSound;
    public AudioClip runningSound;
    private GuardState currentState = GuardState.Patrol;
    private bool playerWasVisible = false;

    void Update()
    {
        bool canSeePlayer = CanSeePlayer();


        if (canSeePlayer && !playerWasVisible)
        {
            PlayerDetectionAudio detectionAudio = player.GetComponentInParent<PlayerDetectionAudio>();

            if (detectionAudio != null)
                detectionAudio.OnPlayerDetected(this);
        }
    
        else if (!canSeePlayer && playerWasVisible)
        {
            PlayerDetectionAudio detectionAudio = player.GetComponentInParent<PlayerDetectionAudio>();

            if (detectionAudio != null)
                detectionAudio.OnPlayerLost(this);
        }

        playerWasVisible = canSeePlayer;

        if (GameManager.Instance != null && GameManager.Instance.alarmActive)
        {
            currentState = GuardState.Chase;
        }
        else
        {
            if (canSeePlayer)
            {
                currentState = GuardState.Chase;
            }
            else if (currentState == GuardState.Chase)
            {
                currentState = GuardState.Patrol;
            }
        }

        UpdateMovementAudio();

        switch (currentState)
        {
            case GuardState.Patrol:
                Patrol();
                break;

            case GuardState.Waiting:
                Waiting();
                break;

            case GuardState.Chase:
                Chase();
                break;
        }
    }
    bool CanSeePlayer()
    {
        if (player == null)
            return false;

        Vector3 origin = eyePoint != null ? eyePoint.position : transform.position + Vector3.up;

        Vector3 target = player.position + Vector3.up;
        Vector3 direction = target - origin;

        float distance = direction.magnitude;

        if (distance > detectionRange)
            return false;

        RaycastHit[] hits = Physics.RaycastAll(origin, direction.normalized, distance);

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.root == transform.root)
                continue;

            return hit.transform.root == player.root;
        }

        return false;
    }
    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        Transform target = patrolPoints[currentPoint];

        if (target == null)
            return;

        MoveTowardsTarget(target.position, patrolSpeed);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < arrivalDistance)
        {
            Debug.Log("Guardia llegó al puinto " + currentPoint);
            currentState = GuardState.Waiting;
            waitTimer = waitTime;

        }
    }
    void Waiting()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
        {
            waitTimer = 0f;
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
            currentState = GuardState.Patrol;
            Debug.Log("Guardia continúa en la patrulla");
        }
    }

    void Chase()
    {
        MoveTowardsTarget(player.position, chaseSpeed);
    }

    void MoveTowardsTarget(Vector3 target, float speed)
    {
        target.y = transform.position.y;

        Vector3 direction = target - transform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Derrota 1.0");
        }
    }
    void UpdateMovementAudio()
    {
        if (movementAudio == null)
            return;

        AudioClip desiredClip = null;

        if (currentState == GuardState.Patrol)
        {
            desiredClip = walkingSound;
        }
        else if (currentState == GuardState.Chase)
        {
            desiredClip = runningSound;
        }

        
        if (desiredClip == null)
        {
            if (movementAudio.isPlaying)
                movementAudio.Stop();

            return;
        }

        
        if (movementAudio.clip != desiredClip || !movementAudio.isPlaying)
        {
            movementAudio.clip = desiredClip;
            movementAudio.loop = true;
            movementAudio.Play();
        }
    }
}

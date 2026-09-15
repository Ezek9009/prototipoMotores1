using System.Xml.Serialization;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public Transform player;
    public float detectionRange = 5f;

    public Color normalColor = Color.white;
    public Color detectedColor = Color.red;
    private int currentPoint = 0;
    private Renderer robotRenderer;

    private void Start()
    {
        robotRenderer = GetComponent<Renderer>();
        robotRenderer.material.color = normalColor;
    }
    private void Update()
    {
        Patrol();
        DetectedPlayer();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0f)
        {
            return;
        }
        Transform target = patrolPoints[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentPoint++;
            if ( currentPoint >= patrolPoints.Length )
            {
                currentPoint = 0;
            }
        }
    }

    void DetectedPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if ( distance <= detectionRange)
        {
            robotRenderer.material.color = detectedColor;
        }
        else
        {
            robotRenderer.material.color = normalColor;
        }
    }
}

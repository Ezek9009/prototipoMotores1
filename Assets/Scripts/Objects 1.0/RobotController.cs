using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RobotController : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed = 2f;
    public Transform player;
    public float detectionRange = 5f;
    public float loseDetectionRange = 8f;

    public Color normalColor = Color.white;
    public Color detectedColor = Color.red;
    public float defeatTime = 5f;
    public TMP_Text timerText;
    private int currentPoint = 0;
    private Renderer robotRenderer;

    public bool playerDetected = false;
    public float timer = 0f;
    private void Start()
    {
        robotRenderer = GetComponent<Renderer>();
        robotRenderer.material.color = normalColor;

        timerText.gameObject.SetActive(false);
    }
    void Update()
    {
        Patrol();
        DetectedPlayer();
        if (playerDetected)
        {
            timer += Time.deltaTime;
            float timeRemaining = defeatTime - timer;
            timerText.text = Mathf.Ceil(timeRemaining).ToString();
            if (timer >= defeatTime)
            {
                SceneManager.LoadScene("Derrota1.0");
            }
        }
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
       if (!playerDetected)
        {
            if(distance <= detectionRange)
            {
                playerDetected = true;
                robotRenderer.material.color = detectedColor;
                timer = 0f;
                timerText.gameObject.SetActive(true);
                Debug.Log("Player detectado");
            }
        }
       else
        {
            if (distance >=loseDetectionRange)
            {
                playerDetected = false;
                robotRenderer.material.color = normalColor;
                timer = 0f;
                timerText.gameObject.SetActive(false);
                Debug.Log("Player escapó");
            }
        }
        
        }
    }


using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class SecurityCamera : MonoBehaviour
{
    [Header("Rotacion")]
    public float rotationAngle = 45f;
    public float rotationSpeed = 30f;
    public float waitTime = 2f;
    private float currentAngle = 0f;
    private bool rotationRight = true;
    private float waitTimer = 0f;

    [Header("Deteccion")]
    public Light spotLight;
    public Transform player;
    public Color normalLightColor = Color.white;
    public Color detectedLightColor = Color.red;
    public Collider playerCollider;
    
    [Header("Contador")]
    public float defeatTime = 5f;
    public TMP_Text timerText;
    private float detectionTimer = 0f;
    private bool playerDetected = false;

    [Header("Sonido")]
    public AudioSource movementAudio;
    public AudioClip panningSound;
    private void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }
        if (spotLight != null)
        {
            spotLight.color = normalLightColor;
        }
    }
    void Update()
    {
        RotateCamera();
        DetectPlayer();
    }

    void RotateCamera()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.deltaTime;

            if (movementAudio != null && movementAudio.isPlaying)
            {
                movementAudio.Stop();
            }

            return;
        }

       
        if (movementAudio != null && !movementAudio.isPlaying)
        {
            movementAudio.clip = panningSound;
            movementAudio.loop = true;
            movementAudio.Play();
        }

        float targetAngle = rotationRight ? rotationAngle : -rotationAngle;

        currentAngle = Mathf.MoveTowards(currentAngle, targetAngle,rotationSpeed * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(0f, currentAngle, 0f);

        
        if (Mathf.Approximately(currentAngle, targetAngle))
        {
            rotationRight = !rotationRight;
            waitTimer = waitTime;

            if (movementAudio != null && movementAudio.isPlaying)
            {
                movementAudio.Stop();
            }
        }
    }
    void DetectPlayer()
    {
        if (spotLight == null || player == null || playerCollider == null)
            return;

        Vector3 targetPosition = playerCollider.bounds.center;
        Vector3 directionToPlayer = targetPosition - spotLight.transform.position;

        float distance = directionToPlayer.magnitude;

        float angle = Vector3.Angle(spotLight.transform.forward, directionToPlayer);

        bool insideCone = distance <= spotLight.range && angle <= spotLight.spotAngle / 2f;
        bool visible = false;

        if (insideCone)
        {
            RaycastHit hit;

            if (Physics.Raycast(spotLight.transform.position, directionToPlayer.normalized, out hit, distance))
            {
                Debug.Log("Cámara detecta: " + hit.transform.name);
                Transform hitObject = hit.transform;
                visible = hitObject == player || hitObject.IsChildOf(player);
            }
        }

        if (visible)
        {
            detectionTimer += Time.deltaTime;
            spotLight.color = detectedLightColor;
            if(timerText != null)
            {
                timerText.gameObject.SetActive(true);
                float remainingTime = defeatTime - detectionTimer;
                timerText.text = Mathf.CeilToInt(remainingTime).ToString();
            }
            if (detectionTimer >= defeatTime)
            {
                SceneManager.LoadScene("Derrota 1.0");
            }
        }
        else
        {
            detectionTimer = 0f;
            spotLight.color = normalLightColor;
            if (timerText != null)
            {
                timerText.gameObject.SetActive(false);
            }
        }
    }
}
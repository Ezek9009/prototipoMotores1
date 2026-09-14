using UnityEngine;
using UnityEngine.InputSystem;//nuevoInputSystem

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]

public class cubeController : MonoBehaviour
{
    public InputAction moveAction;

    void OnEnable()
    {
        moveAction.Enable(); // habilita la accion
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    private CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent <PlayerInput>();

        //acciones definidas en tu Input Actions asset
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["look"];
    }

    // Update is called once per frame
    void Update()
    {
        // --- Movimiento con wasd / joystick ---
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        Debug.Log("Input: " + input);

        // --- Rotación con mouse / joystick ---
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * lookInput.x * rotationSpeed * Time.deltaTime);

    }
}

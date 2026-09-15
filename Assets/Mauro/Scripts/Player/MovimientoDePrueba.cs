using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            input = new Vector2(Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue(), Keyboard.current.wKey.ReadValue() - Keyboard.current.sKey.ReadValue());
        }

        Vector3 movement = transform.right * input.x + transform.forward * input.y;
           
        transform.position += movement * speed * Time.deltaTime;
    }
}
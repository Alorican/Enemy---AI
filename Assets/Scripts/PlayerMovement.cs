using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (Keyboard.current == null) return;

        float moveX = (Keyboard.current.dKey.isPressed ? 1f : 0f)
                    - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float moveZ = (Keyboard.current.wKey.isPressed ? 1f : 0f)
                    - (Keyboard.current.sKey.isPressed ? 1f : 0f);

        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        if (movement.magnitude > 1f)
            movement.Normalize();

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
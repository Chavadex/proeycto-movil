using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public FloatingJoystick joystick;

    private Rigidbody2D rb;
    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        input = joystick.Direction;
    }

    void FixedUpdate()
    {

        rb.linearVelocity = input * speed;
    }
}

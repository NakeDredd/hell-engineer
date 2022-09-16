using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    [SerializeField] private float groundCheckHeight;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int horizontalSlowForce;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundCheck;

    private Rigidbody2D rb;
    private int currentSpeed;

    private float moveInput;
    private float coyoteTimeCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentSpeed = speed;

        InputRegister.Instance.InputMovement += Move;
    }
    private void OnDisable()
    {
        InputRegister.Instance.InputMovement -= Move;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }
    }

    private void Move(float directionX)
    {
        moveInput = directionX * currentSpeed;

        Vector2 moveVector = new Vector2(moveInput * Time.fixedDeltaTime, rb.velocity.y);

        rb.velocity = moveVector;

        if (moveInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }
    
    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckHeight, groundCheck.value).collider != null;

        return isGrounded; 
    }
}

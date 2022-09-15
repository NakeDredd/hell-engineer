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
    }

    private void FixedUpdate()
    {
        Move();

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.fixedDeltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && coyoteTimeCounter > 0f)
        {
            Jump(true);

        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            Jump(false);
            coyoteTimeCounter = 0f;
        }
    }

    private void Move()
    {
        moveInput = Input.GetAxis("Horizontal") * currentSpeed;

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

    private void Jump(bool isCoyote)
    {
        if (isCoyote)
        {
            Vector2 jumpVector2 = new Vector2(rb.velocity.x, jumpForce);
            rb.velocity = jumpVector2;
        }else
        {
            Vector2 jumpVector2 = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    
    public bool IsGrounded()
    {
        bool isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckHeight, groundCheck.value).collider != null;
        return isGrounded; 
    }
}

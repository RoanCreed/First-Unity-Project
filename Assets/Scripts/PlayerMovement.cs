using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Animator animator;

    private GameObject player; // Empty GameObject that serves as the parent
    private Transform[] childTransforms;

    private void Start()
    {
        animator = GetComponent<Animator>();




        player = new GameObject("PlayerContainer");
        player.transform.position = transform.position;
        transform.SetParent(player.transform);

        // Get references to child objects that you want to keep unaffected
        childTransforms = new Transform[]
        {
            transform.Find("GroundCheck"), // Replace with your actual child object names
            transform.Find("RotatePoint")
            // Add more child objects as needed
        };

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        
        UpdateAnimation();
        
        Flip();
    }

    private void UpdateAnimation()
    {
        if (horizontal > 0f || horizontal < 0f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (horizontal > 0f)
        {
            FlipParent(false);
        }
        else if (horizontal < 0f)
        {
            FlipParent(true);
        }
    }

    private void FlipParent(bool flip)
    {

        foreach (Transform childTransform in childTransforms)
        {
            childTransform.SetParent(null);
        }

        Vector3 localScale = transform.localScale;
        localScale.x = Mathf.Abs(localScale.x) * (flip ? -1f : 1f);
        transform.localScale = localScale;

        foreach (Transform childTransform in childTransforms)
        {
            childTransform.SetParent(transform);
        }

    }
}
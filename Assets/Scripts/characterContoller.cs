using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterContoller : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private Rigidbody2D rigidbody2D;
    public Animator animator;

    // Grounded variables
    private bool grounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
      
        var movement = Input.GetAxis("Horizontal");
        if (movement < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Flip the character horizontally back if moving right
        else if (movement > 0)
        {
            spriteRenderer.flipX = false;
        }
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
        
        animator.SetFloat("Speed", Mathf.Abs(movement));

        // Check if grounded
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //animator.SetBool("IsGrounded", grounded);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rigidbody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }
		if (grounded == false)
		{
            animator.SetBool("IsJumping", false);
        }
		
        
    }
    

    // Draw the ground check circle in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
}

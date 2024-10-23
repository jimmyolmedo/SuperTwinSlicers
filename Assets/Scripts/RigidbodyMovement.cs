using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    [SerializeField] float speed = 200;
    [SerializeField] bool canMove = true;
    [SerializeField] float jumpForce = 10;
    Vector2 move;
    [SerializeField]Rigidbody2D rb;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 groundCheckerBoxSize;
    [SerializeField] float groundCheckerCastDistance;

    [Header("checkWall")]
    [SerializeField] Vector2 wallChekerBoxSize;
    [SerializeField] float wallCheckerCastDistance;

    [Header("wallJump")]
    [SerializeField] float wallJumpForce;
    [SerializeField] Vector2 wallJumpingPower = new Vector2(8f, 16f);
    [SerializeField] float wallJumpingDirection;
    [SerializeField] float wallJumpDuration;
    [SerializeField] bool wallJumped;


    private void OnEnable()
    {
        InputManager.OnMove += Move;
        InputManager.OnJump += Jump;
        InputManager.OnJump += WallJump;
    }

    private void OnDisable()
    {
        InputManager.OnMove -= Move;
        InputManager.OnJump -= Jump;
        InputManager.OnJump -= WallJump;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
            flip();
        }

        WallSlide();

    }

    void WallSlide()
    {
        if (WallDetected())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -3f));  // Limitar la velocidad de caída
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    private void Update()
    {
        if (isGrounded())
        {
            wallJumped = false;
        }
    }

    void Move(Vector2 _input)
    {
        float x = _input.x;
        float y = _input.y;

        move = new Vector2(x, y);
    }

    void flip()
    {
        if(move.x != 0)
        {
            Vector3 localscale = transform.localScale;
            localscale.x = move.x;
            transform.localScale = localscale;
        }
    }

    void Jump()
    {
        if (isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    void WallJump()
    {
        if (!isGrounded() && !wallJumped)
        {
            if (WallDetected())
            {
                canMove = false;
                wallJumpingDirection = -transform.localScale.x;
                rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);

                if(transform.localScale.x != wallJumpingDirection)
                {
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;
                }

                wallJumped = true;
                Invoke(nameof(RecoverMovement), wallJumpDuration);
            }
        }
    }

    void RecoverMovement()
    {
        canMove = true;
    }

    bool isGrounded()
    {
        if(Physics2D.BoxCast(transform.position, groundCheckerBoxSize, 0, -transform.up, groundCheckerCastDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool WallDetected()
    {
        if (Physics2D.BoxCast(transform.position, wallChekerBoxSize, 0, -transform.right, wallCheckerCastDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position-transform.up * groundCheckerCastDistance, groundCheckerBoxSize);
        Gizmos.DrawWireCube(transform.position-transform.right * wallCheckerCastDistance, wallChekerBoxSize);
    }

}

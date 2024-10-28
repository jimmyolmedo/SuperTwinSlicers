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

    void FixedUpdate()
    {
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
        flip();

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

    public void Move(Vector2 _input)
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

    public void Jump()
    {
        if (isGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    public void WallJump()
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

                wallJumped = false;
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

    public void InvokeInpulse(Vector3 _direction, float _force, float _impulseDuration)
    {
        canMove = false;
        rb.velocity = new Vector2(_direction.x * _force, _direction.y * _force);

        Invoke(nameof(RecoverMovement), _impulseDuration);

        //rb.AddForce(_direction * _force);
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

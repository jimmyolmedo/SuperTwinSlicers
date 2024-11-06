using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Okiyo : Character, Idamageable
{

    [SerializeField] RigidbodyMovement rM;
    [SerializeField] AttackPlayer Attack;
    [SerializeField] bool canMove;
    [SerializeField] Vector2 groundCheckerBoxSize;
    [SerializeField] float groundCheckerCastDistance;
    [SerializeField] LayerMask groundLayer;

    protected override void Awake()
    {
        base.Awake();
        currenLife = maxLife;
    }

    private void OnEnable()
    {
        InputManager.OnMove += Move;
        InputManager.OnJump += Jump;
        InputManager.OnJump += rM.WallJump;
        InputManager.OnStartedAirAttack += Attack.StartedAirAttack;
        InputManager.OnEndedAirAttack += Attack.EndedAirAttack;
        InputManager.OnNormalAttack += NormalAttack;
    }

    private void OnDisable()
    {
        InputManager.OnMove -= Move;
        InputManager.OnJump -= Jump;
        InputManager.OnJump -= rM.WallJump;
        InputManager.OnStartedAirAttack -= Attack.StartedAirAttack;
        InputManager.OnEndedAirAttack -= Attack.EndedAirAttack;
        InputManager.OnNormalAttack -= NormalAttack;
    }

    void Move(Vector2 _input)
    {
        if(canMove)
        {
            rM.Move(_input);
        }
    }

    void Jump()
    {
        rM.Jump();
    }
    
    void NormalAttack()
    {
        Attack.NormalAttack();
    }

    protected override void Die()
    {
        base.Die();
    }


    void GetDamage()
    {

    }

    private void OnDrawGizmos()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Okiyo : Character
{
    [SerializeField] RigidbodyMovement rM;
    [SerializeField] AttackPlayer Attack;
    [SerializeField] bool canMove;

    private void Awake()
    {
        currenLife = maxLife;
    }

    private void OnEnable()
    {
        InputManager.OnMove += Move;
        InputManager.OnJump += rM.Jump;
        InputManager.OnJump += rM.WallJump;
        InputManager.OnStartedAirAttack += Attack.StartedAirAttack;
        InputManager.OnEndedAirAttack += Attack.EndedAirAttack;
    }

    private void OnDisable()
    {
        InputManager.OnMove -= Move;
        InputManager.OnJump -= rM.Jump;
        InputManager.OnJump -= rM.WallJump;
        InputManager.OnStartedAirAttack -= Attack.StartedAirAttack;
        InputManager.OnEndedAirAttack -= Attack.EndedAirAttack;
    }

    void Move(Vector2 _input)
    {
        if(canMove)
        {
            rM.Move(_input);
        }
    }

    protected override void Die()
    {
        base.Die();
    }

}

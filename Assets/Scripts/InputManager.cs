using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControlScheme
{
    PC,
    Gamepad
}

public class InputManager : MonoBehaviour
{

    [SerializeField] PlayerInput playerInput;
    
    public static event System.Action<Vector2> OnMove;
    public static event System.Action OnJump;
    public static event System.Action OnShoot;
    public static event System.Action OnDash;

    public static ControlScheme CurrentScheme {get; private set;}

    public static System.Action<ControlScheme> onSchemeSwitch;

    string currenScheme;


    private void OnEnable()
    {
        playerInput.onActionTriggered += HandleInput;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= HandleInput;
    }

    private void Update()
    {
        CheckControlScheme();
    }

    public void HandleInput(InputAction.CallbackContext context)
    {

        TryInvokeMove(context);
        TryInvokeJump(context);
        TryInvokeShoot(context);
        TryInvokeDash(context);
    }

    void CheckControlScheme()
    {
        if(playerInput.currentControlScheme == currenScheme) return;
        currenScheme = playerInput.currentControlScheme;

        if(currenScheme == "Gamepad")
        {
            CurrentScheme = ControlScheme.Gamepad;
        }
        else
        {
            CurrentScheme =ControlScheme.PC;
        }

        onSchemeSwitch?.Invoke(CurrentScheme);
    }

    void TryInvokeMove(InputAction.CallbackContext context)
    {
        if(context.action.name != "Move")return;

        
        Vector2 direction = context.ReadValue<Vector2>();
        OnMove?.Invoke(direction);
    }

    void TryInvokeJump(InputAction.CallbackContext context)
    {
        if(context.action.name != "Jump") return;

        if(context.started)
        {
            OnJump?.Invoke();
        }
    }

    void TryInvokeShoot(InputAction.CallbackContext context)
    {
        if (context.action.name != "Shoot") return;

        OnShoot?.Invoke();
    }

    void TryInvokeDash(InputAction.CallbackContext context)
    {
        if (context.action.name != "Dash") return;

        if (context.started)
        {
            OnDash?.Invoke();
        }
    }


}

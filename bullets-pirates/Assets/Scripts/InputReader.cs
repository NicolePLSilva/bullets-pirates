using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{

    public Vector2 MovementValue { get; private set; }
    
    public event Action OnAttackEvent;
    public event Action OnLeftAttackEvent;
    public event Action OnRightAttackEvent;

    private Controls controls;

    void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();    
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnAttackEvent?.Invoke();
        }
    }

    public void OnLeftCannonsTripleAttack(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            OnLeftAttackEvent?.Invoke();
        }
    }

    public void OnRightCannonsTripleAttack(InputAction.CallbackContext context)
    {
        if(context.started) 
        {
            OnRightAttackEvent?.Invoke();
        }
    }
}

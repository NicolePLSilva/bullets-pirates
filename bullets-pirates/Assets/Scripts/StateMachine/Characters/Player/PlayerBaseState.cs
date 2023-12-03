using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : CharacterBaseState
{
    public PlayerBaseState(CharacterStateMachine stateMachine): base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.OnAttackEvent += OnAttack;
        stateMachine.InputReader.OnLeftAttackEvent += OnLeftAttack;
        stateMachine.InputReader.OnRightAttackEvent += OnRightAttack;
        stateMachine.Health.OnDie += OnDieProcess;
        stateMachine.Health.OnTakeDamage += TakeDamageProcess;
    }

    public override void InProgress(float deltaTime)
    { 
       CalculateMovement(deltaTime);
    }

    public override void Exit() 
    { 
        stateMachine.InputReader.OnAttackEvent -= OnAttack;
        stateMachine.InputReader.OnLeftAttackEvent -= OnLeftAttack;
        stateMachine.InputReader.OnRightAttackEvent -= OnRightAttack;
    }

    private void CalculateMovement(float deltaTime)
    {
         float rotation = stateMachine.InputReader.MovementValue.x * stateMachine.RotationSpeed * deltaTime;

        Vector2 moveDirection = Vector2.up * stateMachine.InputReader.MovementValue.y * stateMachine.MovementSpeed;

        Transform pivotPoint = stateMachine.InputReader.MovementValue.x > 0 ? stateMachine.RightPivot : stateMachine.LeftPivot; 
        Vector3 forward = stateMachine.InputReader.MovementValue.y > 0 ? Vector3.up : Vector3.zero; 

        stateMachine.Rigidbody2D.velocity = Vector2.zero;

        Vector3 point = new Vector3(pivotPoint.position.x, pivotPoint.position.y,0);
        Vector3 axis = new Vector3(0,0, -rotation);
        stateMachine.transform.RotateAround(point, axis, deltaTime * stateMachine.RotationSpeed);        


        stateMachine.transform.Translate(-forward * deltaTime, Space.Self);
    }

    private void OnAttack()
    {
        Attack(stateMachine.transform, stateMachine.BaseDamage);
    }

    private void OnLeftAttack()
    {
        Attack(stateMachine.SideShootTransform, -1, stateMachine.BaseDamage);
    }

    private void OnRightAttack()
    {
        Attack(stateMachine.SideShootTransform, 1, stateMachine.BaseDamage);
    }

    private void OnDieProcess()
    {
        Die();
        stateMachine.InputReader.enabled = false;
        SessionManager.Instance.SessionOver();
    }
   
    private void TakeDamageProcess()
    {
        TakeHit();
    }
}

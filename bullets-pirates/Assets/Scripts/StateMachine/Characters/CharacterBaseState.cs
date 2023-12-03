using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBaseState : State
{   
    protected CharacterStateMachine stateMachine;

    public CharacterBaseState(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector2.zero, deltaTime);
    }

    protected void Move(Vector2 motion, float deltaTime)
    {
        stateMachine.Rigidbody2D.velocity = motion;
    }

    protected void Attack(Transform bulletSpawnPoint, int damage)
    {
        bool successfulShoot = stateMachine.Shooter.Shoot(bulletSpawnPoint, damage);
        if (!successfulShoot) { return; }
        stateMachine.Animator.Play("SingleShoot", 0, 0.2f);
    }

    protected void Attack(Transform bulletSpawnPoint, float orientation, int damage)
    {
        bool successfulShoot = stateMachine.Shooter.SideShoot(bulletSpawnPoint, orientation, damage);
        if (!successfulShoot) { return; }
        string shootAnimString = orientation > 0 ? "RightCannons" : "LeftCannons";
        stateMachine.Animator.Play(shootAnimString, 0, 0.2f);
    }

    protected void TakeHit()
    {
        stateMachine.Animator.Play("TakeDamage", 0, 0f);
    }

    protected void Die()
    {
        stateMachine.Animator.Play("Death", 0, 0f);
    }

    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterState : EnemyBaseState
{   
    private Quaternion lastParentRotation;
    private float shootTimer;
    private float shootDistance;

    public EnemyShooterState(CharacterStateMachine stateMachine): base (stateMachine) { }
    
    public override void Enter()
    {
        lastParentRotation = stateMachine.FrontalShootTransform.parent.localRotation;    
        shootTimer = stateMachine.GetComponent<EnemyType>().shootRate;
        shootDistance = stateMachine.GetComponent<EnemyType>().shootDistance;
    } 

    public override void InProgress(float deltaTime)
    {   
        shootTimer -= deltaTime;
        CalculateCannonRotation();
        CalculateShipMovement(deltaTime);
    } 

    public override void Exit() { } 

    private void CalculateCannonRotation()
    {
        IgnoreCannonParentRotation();
        if (CheckDistanceToPlayer(shootDistance)) 
        { 
            Vector3 relative = stateMachine.FrontalShootTransform.InverseTransformPoint(stateMachine.Player.position);
            float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
            stateMachine.FrontalShootTransform.Rotate(0, 0, angle);
            
            if (shootTimer <= 0 && !stateMachine.Health.IsDead) { TryShoot(); }
        }
    }

    private void TryShoot()
    {
        stateMachine.FrontalShootTransform.up = -stateMachine.FrontalShootTransform.right;
        Attack(stateMachine.FrontalShootTransform,  stateMachine.BaseDamage);
        shootTimer = stateMachine.GetComponent<EnemyType>().shootRate;
    }

    private void CalculateShipMovement(float deltaTime)
    {
        Vector3 point = new Vector3(stateMachine.LeftPivot.position.x,stateMachine.LeftPivot.position.y,0);
        Vector3 axis = new Vector3(0,0, 1);
        stateMachine.transform.RotateAround(point, axis, deltaTime * stateMachine.RotationSpeed);
    }

    private void IgnoreCannonParentRotation()
    {
        stateMachine.FrontalShootTransform.localRotation = Quaternion.Inverse(stateMachine.FrontalShootTransform.parent.localRotation)
                                                                            * lastParentRotation 
                                                                            * stateMachine.FrontalShootTransform.localRotation;

        lastParentRotation = stateMachine.FrontalShootTransform.parent.localRotation;
    }
}

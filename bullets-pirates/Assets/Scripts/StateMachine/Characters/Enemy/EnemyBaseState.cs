using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : CharacterBaseState
{
    public EnemyBaseState(CharacterStateMachine stateMachine): base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Health.OnDie += OnDieProcess;
        stateMachine.Health.OnTakeDamage += TakeDamageProcess;
    }
    

    public override void InProgress(float deltaTime)
    {
        if(stateMachine.GetComponent<EnemyType>().enemyType == EnemyType.Type.Shooter)
        {
            stateMachine.SwitchState(new EnemyShooterState(stateMachine));
        }
        else if (stateMachine.GetComponent<EnemyType>().enemyType == EnemyType.Type.Chaser)
        {
            stateMachine.SwitchState(new EnemyChaserState(stateMachine));
        }
    } 

    public override void Exit() { } 

    protected bool CheckDistanceToPlayer(float distance)
    {
        Vector3 distanceToPlayer = stateMachine.FrontalShootTransform.position - stateMachine.Player.position;
        float offset = distanceToPlayer.sqrMagnitude;
        return offset <= distance;
    }

    private void OnDieProcess()
    {
        Die();
        stateMachine.Player.GetComponent<Score>().scorePoints++;
    }

    private void TakeDamageProcess()
    {
        TakeHit();
    }

}

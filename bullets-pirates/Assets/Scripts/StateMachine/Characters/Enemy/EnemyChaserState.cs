using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserState : EnemyBaseState
{
    private float chaserVelocity;
    private float explosionDistance;         
    
    public EnemyChaserState(CharacterStateMachine stateMachine): base (stateMachine) { }
    
    public override void Enter()
    {
        explosionDistance = stateMachine.GetComponent<EnemyType>().explosionDistance;
        chaserVelocity = stateMachine.MovementSpeed;
    } 

    public override void InProgress(float deltaTime)
    {   
        
        Move(CalculateMovement(), deltaTime);
        
        LookForPlayer();

        if (!stateMachine.Health.IsDead == true && CheckDistanceToPlayer(explosionDistance))
        {
            ExplodeProcess();
        }

    } 

    public override void Exit() { } 

    private Vector2 CalculateMovement()
    {
        var x = stateMachine.Player.position.x - stateMachine.transform.position.x;
        var y = stateMachine.Player.position.y - stateMachine.transform.position.y;
        return new Vector2(x * chaserVelocity / 40, y * chaserVelocity / 40);
    }

    private void LookForPlayer()
    {
        Vector3 relative = stateMachine.transform.InverseTransformPoint(stateMachine.Player.position);
            float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
            stateMachine.transform.Rotate(0, 0, angle);
        stateMachine.transform.up = -stateMachine.transform.right;
    }

    private void ExplodeProcess()
    {
        stateMachine.Player.GetComponent<Health>().ApllyDamage(stateMachine.BaseDamage);
            stateMachine.Health.ApllyDamage((int)stateMachine.Health.MaxHealth);
            stateMachine.Player.GetComponent<Score>().scorePoints--;
    }
}

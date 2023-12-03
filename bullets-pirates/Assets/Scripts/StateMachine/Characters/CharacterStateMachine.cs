using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    [field:Header("General Config")]
    [field: SerializeField] public InputReader InputReader{ get; private set; }
    [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    [field: SerializeField] public Collider2D Collider { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Transform Player { get; private set; }
    [field:Header("Movement Config")]
    [field: SerializeField] public Transform LeftPivot{ get; private set; }
    [field: SerializeField] public Transform RightPivot{ get; private set; }
    [field: SerializeField] public float MovementSpeed{ get; private set; }
    [field: SerializeField] public float RotationSpeed{ get; private set; }
    [field:Header("Attack Config")]
    [field: SerializeField] public Shooter Shooter{ get; private set; }
    [field: SerializeField] public Transform FrontalShootTransform{ get; private set; }
    [field: SerializeField] public Transform SideShootTransform{ get; private set; }
    [field: SerializeField] public int BaseDamage{ get; private set; }

    void Start()
    {
        if (InputReader == null)
        {
            if (Player == null)
            {
                Player = FindObjectOfType<InputReader>().transform;
            }
            SwitchState(new EnemyBaseState(this));               
        }
        else
        {
            SwitchState(new PlayerBaseState(this));               
            
        }
    }

}

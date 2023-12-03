using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    public enum Type
    {
        Shooter,
        Chaser
    }

    [SerializeField]public Type enemyType;
    [Header("Shooter Ship")]
    [SerializeField]public float shootRate = 3f;
    [SerializeField]public float shootDistance = 3f;
    [SerializeField]public int shootDamage = 3;
    [Header("Chaser Ship")]
    [SerializeField]public float explosionDistance = 3f;
    [SerializeField]public int explosionDamage = 3;
}

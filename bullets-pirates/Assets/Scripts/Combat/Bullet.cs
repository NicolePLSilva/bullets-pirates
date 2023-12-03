using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public Collider2D myCollider;
    [SerializeField] public int damage = 1;

    
    [SerializeField] private float bulletLifeTimer = 4f;
    [SerializeField] private float CurrentBulletLifeTimer;

    private void OnEnable()
    {
        CurrentBulletLifeTimer = bulletLifeTimer;
    }

    private void Update()
    {
        CurrentBulletLifeTimer -= Time.deltaTime;

        if(CurrentBulletLifeTimer >= 0) { return; }

        this.gameObject.SetActive(false);
    }

    public void SetOriginCollider(Collider2D collider)
    {
        myCollider = collider;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other == myCollider) { return; }

        if(other.TryGetComponent<Health>(out Health health))
        {
            health.ApllyDamage(damage);
        }   

        this.gameObject.SetActive(false);

    }

}

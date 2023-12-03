using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Image sigleShootCooldownImage;
    [SerializeField] private Image tripleShootCooldownImage;

    public GameObject BulletsPrefab;
    public float shootSpeed = 10;
    public int bulletsAmount = 32;

    public float singleShootCooldown = 1f;
    public float tripleShootCooldown = 4f;


    private Rigidbody2D rb;
    private ObjectPool bulletPool;

    private float initSingleCooldown;
    private float initTripleCooldown;

    private void Awake()
    {
        bulletPool = GetComponentInChildren<ObjectPool>();
        bulletPool.PopulatePool(BulletsPrefab,  this.transform, bulletsAmount);
    }

    private void Start()
    {
        initSingleCooldown = singleShootCooldown;
        initTripleCooldown = tripleShootCooldown;
    }

    private void Update()
    {
        singleShootCooldown -= Time.deltaTime;
        tripleShootCooldown -= Time.deltaTime;

        if (sigleShootCooldownImage == null ) { return; }
        UpdateShootCanvas();
    }

    public bool Shoot(Transform spawnTransform, int damage)
    { 
        if (singleShootCooldown > 0) { return false; }

        GameObject bullet = bulletPool.RequestObjectInPool(spawnTransform); 
        
        Collider2D collider = spawnTransform.GetComponent<Collider2D>();
        if (collider == null) 
        { 
            collider = spawnTransform.parent.GetComponent<Collider2D>();
        }
        
        bullet.GetComponent<Bullet>().SetOriginCollider(collider);
        bullet.GetComponent<Bullet>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(-bullet.transform.up * shootSpeed, ForceMode2D.Impulse); 
        bullet.transform.parent = null;

        singleShootCooldown = initSingleCooldown;
        return true;
    }

    public bool SideShoot(Transform spawnTransform, float orientation, int damage)
    {   
        if (tripleShootCooldown > 0) { return false; }

        foreach (Transform child in spawnTransform)
        {
            GameObject bullet = bulletPool.RequestObjectInPool(child);

            bullet.GetComponent<Bullet>().SetOriginCollider(spawnTransform.parent.GetComponent<Collider2D>());
            bullet.GetComponent<Bullet>().damage = damage;
            bullet.GetComponent<Rigidbody2D>().AddForce(-bullet.transform.right * shootSpeed * orientation, ForceMode2D.Impulse);   
            bullet.transform.parent = null;  
        }
        tripleShootCooldown = initTripleCooldown;
        return true;
    }

    private void UpdateShootCanvas()
    {
        sigleShootCooldownImage.fillAmount = 1 - (singleShootCooldown / initSingleCooldown);
        tripleShootCooldownImage.fillAmount = 1 - (tripleShootCooldown / initTripleCooldown);
    }

}

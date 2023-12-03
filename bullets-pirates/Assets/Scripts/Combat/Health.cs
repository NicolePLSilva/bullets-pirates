using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [field:SerializeField] public float MaxHealth { get; private set; }
    [field:SerializeField] public float CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth == 0;

    [SerializeField] private Image healthBar;
    [SerializeField] private Transform healthCanvasTransform;
    [SerializeField] private SpriteRenderer currentShipStateSprites;
    [SerializeField] private Sprite[] shipStateSprites;


    private Quaternion initialRotation;
    private Vector3 initialPosition;
    private Vector3 initScale;
    private Color initSpriteColor;

    public event Action OnTakeDamage;
    public event Action OnDie;
    
    private void OnEnable()
    {
        CurrentHealth = MaxHealth;
        healthCanvasTransform.gameObject.SetActive(true);
    }

    private void Start()
    {
        initialRotation = healthCanvasTransform.rotation;
        initialPosition = healthCanvasTransform.position;
    }

    private void Update()
    {
        CalculateHealthCanvasTransform();
        UpdateHealthBarCanvas();
        UpdateShip();
    }

    public void ApllyDamage(int damage)
    {
        if(CurrentHealth == 0) { return; }

        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

        OnTakeDamage?.Invoke();

        if (CurrentHealth == 0)
        {
            OnDie?.Invoke();
        }
    }

    private void CalculateHealthCanvasTransform()
    {
        healthCanvasTransform.position = new Vector3(this.transform.position.x, this.transform.position.y +0.75f, this.transform.position.z);
        healthCanvasTransform.rotation = initialRotation;
    }

    private void UpdateHealthBarCanvas()
    {
        healthBar.fillAmount = CurrentHealth * 1/ MaxHealth;
    }

    private void UpdateShip()
    {
        if (CurrentHealth <= (MaxHealth * 0.75))
        { 
            healthBar.color = Color.yellow;
            currentShipStateSprites.sprite = shipStateSprites[1];
        }
        else
        {
            healthBar.color = Color.green;
            currentShipStateSprites.sprite = shipStateSprites[0];
        }

        if (CurrentHealth <= (MaxHealth * 0.50))
        {
            healthBar.color = new Color(1.0f, 0.5f, 0.0f);
            currentShipStateSprites.sprite = shipStateSprites[2];
        }
        if (CurrentHealth <= (MaxHealth * 0.25))
        {
            healthBar.color = Color.red;
            currentShipStateSprites.sprite = shipStateSprites[3];
        }
        if (CurrentHealth <= (MaxHealth * 0))
        {
            healthCanvasTransform.gameObject.SetActive(false);
            currentShipStateSprites.sprite = shipStateSprites[3];
        }
        
    }

    public void DisableInDeathAnimation()
    {
        this.gameObject.SetActive(false);   
    }
   
}

using System;
using UnityEngine;

public abstract class BaseAvatar : MonoBehaviour {

    [SerializeField]
    private int maxHealth = 10;

    [SerializeField]
    private float maxEnergy = 10f;

    [SerializeField]
    private float energyRegeneration = 1f;
    [SerializeField]
    private float slowEnergyRegenerationMultiplier = 0.5f;

    public int MaxHealth => maxHealth;
    public float MaxEnergy => maxEnergy;
    public float EnergyRegeneration => energyRegeneration;
    public float SlowEnergyRegenerationMultiplier => slowEnergyRegenerationMultiplier;

    private bool refillToMaxEnergy;

    private bool canRegenEnergy = true;

    public bool AllowEnergyRegeneration {
        get => canRegenEnergy;
        set => canRegenEnergy = value;
    }

    public float CurrentEnergy {
        get;
        private set;
    }

    public int CurrentHealth {
        get;
        private set;
    }
    
    public delegate void OnDeath();
    public event OnDeath OnDeathEvent; 
    
    public delegate void OnHurt(int damageAmount);
    public event OnHurt OnHurtEvent; 

    
    // Start is called before the first frame update
    void OnEnable() {
        CurrentHealth = maxHealth;
        CurrentEnergy = maxEnergy;
    }

    protected virtual void Update() {
        // if we need to refill to max, force regen
        if (refillToMaxEnergy || AllowEnergyRegeneration) {
            if (CurrentEnergy < MaxEnergy) {
                float energyRegenerationMultiplier = refillToMaxEnergy ? SlowEnergyRegenerationMultiplier : 1.0f;
                CurrentEnergy += EnergyRegeneration * Time.deltaTime * energyRegenerationMultiplier;

                if (CurrentEnergy >= MaxEnergy) {
                    refillToMaxEnergy = false;
                    CurrentEnergy = MaxEnergy;
                }
            }
        }
    }

    public virtual void Hurt(int amount) {
        if (amount < 0)
            return;
        CurrentHealth -= amount;
        OnHurtEvent?.Invoke(amount);
        if (CurrentHealth < 0) {
            Die();
        }
    }

    protected void InvokeDeathEvent() {
        OnDeathEvent?.Invoke();
    }

    protected virtual void Die() {
        InvokeDeathEvent();
        Destroy(gameObject);
    }

    public virtual void Heal(int amount) {
        if (amount > 0)
            return;
        CurrentHealth += amount;
        if (CurrentHealth > maxHealth) {
            CurrentHealth = maxHealth;
        }
    }

    public bool HasEnergy() {
        return !refillToMaxEnergy && CurrentEnergy > 0;
    }
    
    public void ConsumeEnergy(float amount) {
        if(amount < 0f)
            return;
        CurrentEnergy -= amount;
        if (CurrentEnergy <= 0f) {
            CurrentEnergy = 0f;
            refillToMaxEnergy = true;
        }
    }
}

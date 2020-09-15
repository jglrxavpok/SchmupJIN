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

    public float Energy {
        get;
        private set;
    }

    public int CurrentHealth {
        get;
        private set;
    }
    
    // Start is called before the first frame update
    void Start() {
        CurrentHealth = maxHealth;
        Energy = maxEnergy;
    }

    private void Update() {
        // if we need to refill to max, force regen
        if (refillToMaxEnergy || AllowEnergyRegeneration) {
            if (Energy < MaxEnergy) {
                float energyRegenerationMultiplier = refillToMaxEnergy ? SlowEnergyRegenerationMultiplier : 1.0f;
                Energy += EnergyRegeneration * Time.deltaTime * energyRegenerationMultiplier;

                if (Energy >= MaxEnergy) {
                    refillToMaxEnergy = false;
                    Energy = MaxEnergy;
                }
            }
        }
    }

    public virtual void Hurt(int amount) {
        if (amount < 0)
            return;
        CurrentHealth -= amount;
        if (CurrentHealth < 0) {
            Die();
        }
    }

    protected virtual void Die() {
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
        return !refillToMaxEnergy && Energy > 0;
    }
    
    public void ConsumeEnergy(float amount) {
        if(amount < 0f)
            return;
        Energy -= amount;
        if (Energy <= 0f) {
            Energy = 0f;
            refillToMaxEnergy = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAvatar : MonoBehaviour {

    [SerializeField]
    private float maxHealth = 10f;

    [SerializeField]
    private float maxEnergy = 10f;

    public float MaxHealth => maxHealth;
    public float MaxEnergy => maxEnergy;

    public float Energy {
        get;
        private set;
    }

    public float CurrentHealth {
        get;
        private set;
    }
    
    // Start is called before the first frame update
    void Start() {
        CurrentHealth = maxHealth;
        Energy = maxEnergy;
    }

    public virtual void Hurt(float amount) {
        if (amount < 0)
            return;
        CurrentHealth -= amount;
        if (CurrentHealth < 0f) {
            // TODO: death
        }
    }

    public virtual void Heal(float amount) {
        if (amount > 0)
            return;
        CurrentHealth += amount;
        if (CurrentHealth > maxHealth) {
            CurrentHealth = maxHealth;
        }
    }
}

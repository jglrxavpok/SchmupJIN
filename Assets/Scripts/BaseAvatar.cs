using System.Collections;
using UnityEngine;

public abstract class BaseAvatar : MonoBehaviour {

    [SerializeField]
    private int maxHealth = 10;

    [SerializeField]
    private int maxEnergy = 10;

    public int MaxHealth => maxHealth;
    public int MaxEnergy => maxEnergy;

    public int Energy {
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
}

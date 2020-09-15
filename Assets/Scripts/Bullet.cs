using System;
using UnityEngine;

[RequireComponent(typeof(Physics))]
public abstract class Bullet : MonoBehaviour {
    [SerializeField] private int damage;

    public int Damage => damage;

    private Physics physics;

    protected Physics Physics => physics;

    private void Start() {
        physics = GetComponent<Physics>();
    }

    private void Update() {
        Move();
    }

    protected abstract void Move();
    
    private void OnTriggerEnter2D(Collider2D other) {
        // filtering is done via layers
        BaseAvatar avatar = other.GetComponent<BaseAvatar>();
        
        if (!avatar) return;
        
        avatar.Hurt(damage);
        Destroy(gameObject);
    }
}
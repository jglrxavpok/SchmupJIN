using System;
using UnityEngine;

[RequireComponent(typeof(Physics))]
public abstract class Bullet : MonoBehaviour {
    [SerializeField] private int damage;

    private PrefabPool sourcePool;

    public PrefabPool SourcePool {
        set => sourcePool = value;
    }

    public int Damage => damage;

    private Physics physics;

    protected Physics Physics => physics;

    protected virtual void Start() {
        physics = GetComponent<Physics>();
        Debug.Assert(sourcePool != null);
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

        if (sourcePool != null) {
            sourcePool.Free(gameObject);
        } else {
            gameObject.SetActive(false);
        }
    }
}
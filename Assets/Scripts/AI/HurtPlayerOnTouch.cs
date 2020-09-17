using System;
using Player;
using UnityEngine;

namespace AI {
    public class HurtPlayerOnTouch : MonoBehaviour {

        [SerializeField]
        private int damage;

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player")) {
                PlayerAvatar playerAvatar = other.gameObject.GetComponent<PlayerAvatar>();
                playerAvatar.Hurt(damage);
                Destroy(gameObject);
            }
        }
    }
}
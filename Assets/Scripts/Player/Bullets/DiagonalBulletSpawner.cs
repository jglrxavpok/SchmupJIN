using System;
using UnityEngine;

namespace Player.Bullets {
    public class DiagonalBulletSpawner : MonoBehaviour {
        private void Update() {
            if(transform.childCount == 0)
                Destroy(gameObject);
        }
    }
}
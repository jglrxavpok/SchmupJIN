using System;
using UnityEngine;

namespace Player {
    
    [RequireComponent(typeof(Physics))]
    public class AlignWithVelocity : MonoBehaviour {
        private Physics physics;

        private void Start() {
            physics = GetComponent<Physics>();
        }

        private void Update() {
            float angle = Vector2.SignedAngle(Vector2.right, physics.Velocity);
            if (!float.IsNaN(angle)) {
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
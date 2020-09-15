using System;
using UnityEngine;

namespace Player {
    public class LimitedMovementEngine : Engine {

        /// <summary>
        /// Portion of the screen the player is allowed in (by default, 10%).
        /// Always counted from the left side of the screen
        /// </summary>
        [SerializeField]
        [Range(0f, 1f)]
        private float limiterRatio = 0.1f;
        
        public float LimiterRatio {
            get => limiterRatio;
            private set => limiterRatio = value;
        }

        private Camera camera;

        private void Start() {
            camera = Camera.main;
        }

        protected override bool IsPositionValid(Vector2 newPosition) {
            // prevent position change if outside of left side of screen
            Vector2 screenPosition = camera.WorldToScreenPoint(newPosition);
            if (screenPosition.x < 0 || screenPosition.y < 0 || screenPosition.x >= Screen.width * LimiterRatio ||
                screenPosition.y >= Screen.height) {
                return false;
            }
            return base.IsPositionValid(newPosition);
        }
    }
}
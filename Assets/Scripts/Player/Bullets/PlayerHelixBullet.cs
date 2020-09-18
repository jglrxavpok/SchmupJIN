using System;
using UnityEngine;

namespace Player.Bullets {
    public class PlayerHelixBullet : Bullet {

        [SerializeField] private float amplitude = 10f;
        [SerializeField] private float speed = 10f;
        [SerializeField] private bool isUp;
        [SerializeField] private float oscillationSpeed = 1.0f;

        public float Speed => speed;

        private float startX;

        protected override void Start() {
            base.Start();
            startX = Physics.Position.x;
        }

        private void OnEnable() {
            if (Physics != null) {
                startX = Physics.Position.x;
            }
        }

        protected override void Move() {
            // pos = Sin(t*m)
            // vel = -Cos(t*m)/m
            float deltaX = Physics.Position.x-startX;
            float verticalVelocity = -Mathf.Cos(deltaX*oscillationSpeed)/(oscillationSpeed) * amplitude;
            Physics.Velocity = new Vector2(Speed, isUp ? verticalVelocity : -verticalVelocity);
        }
    }
}
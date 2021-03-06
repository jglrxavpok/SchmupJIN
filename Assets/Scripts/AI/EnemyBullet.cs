﻿using UnityEngine;

namespace AI {
    public class EnemyBullet : Bullet {
        [SerializeField] private float speed = 10f;

        public float Speed => speed;

        protected override void Move() {
            // TODO: other movements?
            this.Physics.Velocity = new Vector2(-Speed, 0);
        }
    }
}
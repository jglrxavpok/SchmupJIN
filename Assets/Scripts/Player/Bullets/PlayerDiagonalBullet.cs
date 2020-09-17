using UnityEngine;

namespace Player.Bullets {
    public class PlayerDiagonalBullet : Bullet {

        [SerializeField] private float speed = 10f;
        [SerializeField] private bool isUp;

        public float Speed => speed;

        protected override void Move() {
            this.Physics.Velocity = new Vector2(Speed, isUp ? Speed : -Speed);
        }
    }
}
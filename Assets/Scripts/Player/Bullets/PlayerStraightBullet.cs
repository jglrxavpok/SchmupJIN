using UnityEngine;

namespace Player.Bullets {
    public class PlayerStraightBullet : Bullet {

        [SerializeField] private float speed = 10f;

        public float Speed => speed;

        protected override void Move() {
            this.Physics.Velocity = new Vector2(Speed, 0);
        }
    }
}
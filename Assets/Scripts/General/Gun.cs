using UnityEngine;

namespace General {
    // TODO: different gun types
    public class Gun : MonoBehaviour {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private float timeBetweenShots;

        /// <summary>
        /// Time, in seconds, between two bullets while keeping the shoot button pressed
        /// </summary>
        public float TimeBetweenShots => timeBetweenShots;

        private float cooldown;
        
        public GameObject BulletPrefab => bulletPrefab;

        private void Fire() {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
            cooldown = TimeBetweenShots;
        }

        private void Update() {
            if (cooldown > 0f) {
                cooldown -= Time.deltaTime;
            } else {
                cooldown = 0f;
            }
        }

        private bool CanShoot() {
            return cooldown <= 0f;
        }

        public void AttemptFire() {
            if (CanShoot()) {
                Fire();
            }
        }
    }
}
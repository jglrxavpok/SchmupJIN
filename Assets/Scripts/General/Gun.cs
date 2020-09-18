using System;
using Player.Bullets;
using UnityEngine;

namespace General {
    // TODO: different gun types
    [RequireComponent(typeof(BaseAvatar))]
    public class Gun : MonoBehaviour {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private float timeBetweenShots;
        [SerializeField] private float energyCost = 1;
        private PrefabPool bulletPool;
        
        /// <summary>
        /// Time, in seconds, between two bullets while keeping the shoot button pressed
        /// </summary>
        public float TimeBetweenShots => timeBetweenShots;
        public float EnergyCost => energyCost;

        private float cooldown;
        private BaseAvatar avatar;

        private void Start() {
            bulletPool = new PrefabPool(bulletPrefab);
            // don't shoot right after spawning
            cooldown = TimeBetweenShots;
            avatar = GetComponent<BaseAvatar>();
        }

        private void Fire() {
            GameObject bullet = bulletPool.Retrieve(transform.position, Quaternion.identity);

            // helix & diag have a root object to contain both up and down variants
            DualBulletSpawner dualSpawner = bullet.GetComponent<DualBulletSpawner>();
            if (dualSpawner != null) {
                dualSpawner.SourcePool = bulletPool;
            } else {
                bullet.GetComponent<KillOffscreen>().SourcePool = bulletPool;
                bullet.GetComponent<Bullet>().SourcePool = bulletPool;
            }
            cooldown = TimeBetweenShots;
            avatar.ConsumeEnergy(EnergyCost);
        }

        private void Update() {
            if (cooldown > 0f) {
                cooldown -= Time.deltaTime;
            } else {
                cooldown = 0f;
            }
        }

        private bool CanShoot() {
            return cooldown <= 0f && avatar.HasEnergy();
        }

        public void AttemptFire() {
            if (CanShoot()) {
                Fire();
            }
        }
    }
}
using System;
using UnityEngine;

namespace General {
    // TODO: different gun types
    [RequireComponent(typeof(BaseAvatar))]
    public class Gun : MonoBehaviour {
        [SerializeField] private GameObject bulletPrefab;

        [SerializeField] private float timeBetweenShots;
        [SerializeField] private float energyCost = 1;

        /// <summary>
        /// Time, in seconds, between two bullets while keeping the shoot button pressed
        /// </summary>
        public float TimeBetweenShots => timeBetweenShots;
        public float EnergyCost => energyCost;
        public GameObject BulletPrefab => bulletPrefab;

        private float cooldown;
        private BaseAvatar avatar;

        private void Start() {
            // don't shoot right after spawning
            cooldown = TimeBetweenShots;
            avatar = GetComponent<BaseAvatar>();
        }

        private void Fire() {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
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
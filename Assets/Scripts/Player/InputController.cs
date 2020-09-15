using General;
using UnityEngine;

namespace Player {
    
    [RequireComponent(typeof(Physics))]
    [RequireComponent(typeof(Gun))]
    [RequireComponent(typeof(PlayerAvatar))]
    public class InputController : MonoBehaviour {
        private Physics physics;
        private Gun gun;
        private PlayerAvatar avatar;
        
        [SerializeField]
        private float horizontalSpeed = 5f;

        [SerializeField]
        private float verticalSpeed = 5f;

        public float HorizontalSpeed {
            get => horizontalSpeed;
            private set => horizontalSpeed = value;
        }
        
        public float VerticalSpeed {
            get => verticalSpeed;
            private set => verticalSpeed = value;
        }

        private void Start() {
            physics = GetComponent<Physics>();
            gun = GetComponent<Gun>();
            avatar = GetComponent<PlayerAvatar>();
        }

        private void Update() {
            Vector2 velocity = new Vector2(Input.GetAxis("Horizontal")*HorizontalSpeed, Input.GetAxis("Vertical")*VerticalSpeed);
            physics.Velocity = velocity;

            bool shouldShoot = Input.GetButton("Shoot");
            if (shouldShoot) {
                gun.AttemptFire();
            }
            avatar.AllowEnergyRegeneration = !shouldShoot;
        }
    }
}
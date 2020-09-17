using General;
using UnityEngine;

namespace Player {
    
    [RequireComponent(typeof(Physics))]
    [RequireComponent(typeof(GunControl))]
    [RequireComponent(typeof(PlayerAvatar))]
    public class InputController : MonoBehaviour {
        private Physics physics;
        private GunControl guns;
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
            guns = GetComponent<GunControl>();
            avatar = GetComponent<PlayerAvatar>();
        }

        private void Update() {
            Vector2 velocity = new Vector2(Input.GetAxis("Horizontal")*HorizontalSpeed, Input.GetAxis("Vertical")*VerticalSpeed);
            physics.Velocity = velocity;

            bool shouldShoot = Input.GetButton("Shoot");
            if (shouldShoot) {
                guns.AttemptFire();
            }

            if (Input.GetButtonDown("Switch weapon")) {
                guns.SwitchFireMode();
            }
            avatar.AllowEnergyRegeneration = !shouldShoot;
        }
    }
}
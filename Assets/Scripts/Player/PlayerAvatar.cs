using UnityEngine;

namespace Player {
    public class PlayerAvatar : BaseAvatar {
        public override void Hurt(float amount) {
            // TODO: invulnerability frames
            base.Hurt(amount);
        }
    }
}
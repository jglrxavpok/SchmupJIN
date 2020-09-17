using UnityEngine;

namespace Player {
    public class PlayerAvatar : BaseAvatar {
        
        public override void Hurt(int amount) {
            // TODO: invulnerability frames
            base.Hurt(amount);
        }

        protected override void Die() {
            base.Die();
        }
    }
}
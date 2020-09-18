using UnityEngine;

namespace AI {
    public class EnemyAvatar : BaseAvatar {

        public PrefabPool SourcePool {
            set;
            private get;
        }
        
        protected override void Die() {
            InvokeDeathEvent();
            SourcePool.Free(gameObject);
        }
    }
}
using UnityEngine;

namespace AI {
    [RequireComponent(typeof(Physics))]
    public class AIStraightEngine : MonoBehaviour {

        private Physics physics;
        
        [SerializeField]
        private float speed = 10f;

        public float Speed {
            get => speed;
            set => speed = value;
        }

        private void Start() {
            physics = GetComponent<Physics>();
        }

        private void Update() {
            physics.Velocity = new Vector2(-Speed, 0);
        }
    }
}
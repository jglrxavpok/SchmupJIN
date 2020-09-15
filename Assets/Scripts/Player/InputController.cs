using UnityEngine;

namespace Player {
    
    [RequireComponent(typeof(Engine))]
    public class InputController : MonoBehaviour {
        private Engine engine;
        
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
            engine = GetComponent<Engine>();
        }

        private void Update() {
            Vector2 velocity = new Vector2(Input.GetAxis("Horizontal")*HorizontalSpeed, Input.GetAxis("Vertical")*VerticalSpeed);
            engine.velocity = velocity;
        }
    }
}
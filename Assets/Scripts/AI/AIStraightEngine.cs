using UnityEngine;
using Random = System.Random;

namespace AI {
    [RequireComponent(typeof(Physics))]
    public class AIStraightEngine : MonoBehaviour {

        private Physics physics;
        
        [SerializeField]
        private float minSpeed = 10f;

        [SerializeField]
        private float maxSpeed = 10f;
        
        private void Start() {
            Random rng = new Random();
            physics = GetComponent<Physics>();
            double speed = rng.NextDouble() * (maxSpeed-minSpeed) + minSpeed;
            physics.Velocity = new Vector2((float) -speed, 0);
        }
    }
}
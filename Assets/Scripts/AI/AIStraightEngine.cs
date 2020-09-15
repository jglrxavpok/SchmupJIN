using System;
using UnityEngine;

namespace AI {
    [RequireComponent(typeof(Engine))]
    public class AIStraightEngine : MonoBehaviour {

        private Engine engine;
        
        [SerializeField]
        private float speed = 10f;

        public float Speed {
            get => speed;
            set => speed = value;
        }

        private void Start() {
            engine = GetComponent<Engine>();
        }

        private void Update() {
            engine.velocity.x = -Speed;
            engine.velocity.y = 0;
        }
    }
}
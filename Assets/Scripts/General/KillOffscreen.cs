using System;
using UnityEngine;

namespace General {
    /// <summary>
    /// Kill gameobject if outside of screen bounds (+margin)
    /// </summary>
    public class KillOffscreen : MonoBehaviour {
        
        /// <summary>
        /// Margin before killing the gameobject (in screen coordinates)
        /// </summary>
        [SerializeField]
        private float margin;

        private Camera camera;

        private void Start() {
            camera = Camera.main;
        }

        private void Update() {
            Vector2 inScreenSpace = camera.WorldToScreenPoint(transform.position);
            if (inScreenSpace.x < -margin || inScreenSpace.x >= Screen.width + margin || inScreenSpace.y < -margin ||
                inScreenSpace.y >= Screen.height + margin) {
                Destroy(gameObject);
            }
        }
    }
}
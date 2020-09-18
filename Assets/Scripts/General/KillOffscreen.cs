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
        protected float margin;

        private Camera camera;
        private PrefabPool sourcePool;

        public PrefabPool SourcePool {
            set => sourcePool = value;
        }

        private void Start() {
            camera = Camera.main;
        }

        private void Update() {
            Vector2 inScreenSpace = camera.WorldToScreenPoint(transform.position);
            if (OutOfBounds(inScreenSpace)) {
                if (sourcePool != null) {
                    sourcePool.Free(gameObject);
                } else {
                    gameObject.SetActive(false);
                }
            }
        }

        protected virtual bool OutOfBounds(Vector2 inScreenSpace) {
            return inScreenSpace.x < -margin || inScreenSpace.x >= Screen.width + margin || inScreenSpace.y < -margin ||
                   inScreenSpace.y >= Screen.height + margin;
        }
    }
}
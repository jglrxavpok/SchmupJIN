using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Bullets {
    public class DualBulletSpawner : MonoBehaviour {

        public PrefabPool SourcePool {
            set;
            private get;
        }

        private Vector2[] startPositions = new Vector2[2];

        private void Awake() {
            for (int i = 0; i < transform.childCount; i++) {
                startPositions[i] = transform.GetChild(i).position;
            }
        }

        private void OnEnable() {
            for (int i = 0; i < transform.childCount; i++) {
                Transform child = transform.GetChild(i);
                child.position = startPositions[i];
                child.position += transform.position;
                child.gameObject.SetActive(true);
            }
        }

        private void Update() {
            int activeChildCount = 0;

            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).gameObject.activeInHierarchy) {
                    activeChildCount++;
                }
            }

            if (activeChildCount == 0) {
                SourcePool.Free(gameObject);
            }
        }
    }
}
using System;
using General;
using Player;
using UnityEngine;

namespace AI {
    public class AIBasicGun : MonoBehaviour {

        private Gun gun;
        
        private void Start() {
            gun = GetComponent<Gun>();
        }

        private void Update() {
            gun.AttemptFire();
        }
    }
}
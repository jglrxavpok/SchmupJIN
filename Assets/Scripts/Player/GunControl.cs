using System;
using General;
using UnityEngine;
using UnityEngine.Assertions;

namespace Player {
    /// <summary>
    /// Allows multiple guns to be controlled.
    /// One at a time
    /// </summary>
    [RequireComponent(typeof(Gun))] // at least one gun
    public class GunControl : MonoBehaviour {

        [SerializeField]
        private Gun[] guns;

        private int selected;

        private void Start() {
            Assert.IsFalse(guns == null || guns.Length == 0);
        }

        public void AttemptFire() {
            guns[selected].AttemptFire();
        }

        /// <summary>
        /// switch between firing modes
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void SwitchFireMode() {
            selected++;
            selected %= guns.Length;
        }
    }
}
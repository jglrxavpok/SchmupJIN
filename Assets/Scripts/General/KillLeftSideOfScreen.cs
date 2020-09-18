using UnityEngine;

namespace General {
    public class KillLeftSideOfScreen : KillOffscreen {
        protected override bool OutOfBounds(Vector2 inScreenSpace) {
            return inScreenSpace.x < -margin;
        }
    }
}
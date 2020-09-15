using System;
using UnityEngine;

public class Physics : MonoBehaviour {

    public Vector2 Position {
        get => transform.position;
        set => transform.position = value;
    }

    public Vector2 Velocity {
        get;
        set;
    }

    private void Update() {
        Vector2 newPosition = Position + Vector2.right * (Velocity.x * Time.deltaTime);
        // separate axises to handle sliding against screen, walls, etc.
        // step X
        if (IsPositionValid(newPosition)) {
            Position = newPosition;
        }
        
        // stepY
        newPosition = Position + Vector2.up * (Velocity.y * Time.deltaTime);
        if (IsPositionValid(newPosition)) {
            Position = newPosition;
        }
    }

    protected virtual bool IsPositionValid(Vector2 newPosition) {
        return true; // only the player is limited in its movement
    }
}
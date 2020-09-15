using System;
using UnityEngine;

public class Engine : MonoBehaviour {

    public Vector2 Position {
        get => transform.position;
        set => transform.position = value;
    }

    public Vector2 velocity;

    private void Update() {
        Vector2 newPosition = Position + Vector2.right * (velocity.x * Time.deltaTime);
        // separate axises to handle sliding against screen, walls, etc.
        // step X
        if (IsPositionValid(newPosition)) {
            Position = newPosition;
        }
        
        // stepY
        newPosition = Position + Vector2.up * (velocity.y * Time.deltaTime);
        if (IsPositionValid(newPosition)) {
            Position = newPosition;
        }
    }

    protected virtual bool IsPositionValid(Vector2 newPosition) {
        return true; // only the player is limited in its movement
    }
}
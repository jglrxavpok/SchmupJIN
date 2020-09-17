using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour {
    private void Update() {
        if (Input.GetButtonDown("Submit")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
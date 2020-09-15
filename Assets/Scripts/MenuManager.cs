using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void StartButton() {
        SceneManager.LoadScene("TestLevel");
    }

    public void QuitButton() {
        Application.Quit();
    }
}
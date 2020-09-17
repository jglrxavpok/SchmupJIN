using AI;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {
    private Text text;
    private int score;

    private void Start() {
        text = GetComponent<Text>();
        GameManager.Instance.OnEnemySpawn += OnEnemySpawnHandler;
    }

    private void OnEnemySpawnHandler(GameObject enemy) {
        enemy.GetComponent<EnemyAvatar>().OnDeathEvent += AddScoreAfterEnemyDeath;
    }

    private void AddScoreAfterEnemyDeath() {
        score += 10;
        UpdateText();
    }

    private void UpdateText() {
        text.text = score.ToString("00000");
    }
}
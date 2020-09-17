using AI;
using Player;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {
    private Text text;
    private int score;
    
    // repris de mon projet d'été: https://github.com/jglrxavpok/ProjetJINSummer/
    public static string format = "00000";
    public float maxTime = 0.5f;
    public float sizeMultiplier = 0.25f;
    private float animationTime = 0.0f;
    private float targetModifier = 1.0f;

    private void Start() {
        text = GetComponent<Text>();
        GameManager.Instance.OnPlayerSpawn += OnPlayerSpawnHandler;
        GameManager.Instance.OnEnemySpawn += OnEnemySpawnHandler;
    }

    private void OnPlayerSpawnHandler(GameObject player) {
        player.GetComponent<PlayerAvatar>().OnHurtEvent += damage => {
            AddToScore(damage * -20);
            UpdateText();
        };
    }

    private void OnEnemySpawnHandler(GameObject enemy) {
        enemy.GetComponent<EnemyAvatar>().OnDeathEvent += AddScoreAfterEnemyDeath;
    }

    private void AddScoreAfterEnemyDeath() {
        AddToScore(10);
        UpdateText();
    }

    private void UpdateText() {
        text.text = score.ToString(format);
    }

    private void AddToScore(int amount) {
        score += amount;
        if (score < 0)
            score = 0;
        if (score >= 100000)
            score = 99999;
        UpdateText();
        animationTime = maxTime;
        if (amount > 0) {
            targetModifier = sizeMultiplier;
        } else {
            targetModifier = -sizeMultiplier;
        }
    }

    private void Update() {
        float textSizeMultiplier = 1.0f;
        if (animationTime > 0) {
            animationTime -= Time.deltaTime;
            textSizeMultiplier += animationTime/maxTime * targetModifier;
        } else {
            animationTime = 0.0f;
        }

        text.transform.localScale = new Vector3(textSizeMultiplier, 1.0f, 1.0f);
    }
}
using System;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 playerPosition;
    
    /// <summary>
    /// Average position of enemies. Y component is randomised
    /// </summary>
    [SerializeField] private Vector2 centerEnemyPosition;
    
    /// <summary>
    /// Maximum absolute value of Y when randomly generating a position
    /// </summary>
    [SerializeField] private float enemySpawnYRandomRange;
    
    /// <summary>
    /// Time in seconds between two enemy spawns
    /// </summary>
    [SerializeField] private float timeBetweenEnemies;

    private GameObject PlayerPrefab => playerPrefab;
    private GameObject EnemyPrefab => enemyPrefab;
    private Vector2 PlayerPosition => playerPosition;
    private Vector2 CenterEnemyPosition => centerEnemyPosition;
    private float TimeBetweenEnemies => timeBetweenEnemies;
    private float EnemySpawnYRandomRange => enemySpawnYRandomRange;

    private static GameManager instance;
    public static GameManager Instance => instance;

    private float cooldown;
    private Random rng;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        rng = new Random();
    }

    private void Start() {
        Instantiate(PlayerPrefab, PlayerPosition, Quaternion.identity);
    }

    private void Update() {
        if (cooldown > 0f) {
            cooldown -= Time.deltaTime;
        } else {
            SpawnEnemy();
            cooldown = TimeBetweenEnemies;
        }
    }

    private void SpawnEnemy() {
        Vector2 position = CenterEnemyPosition;
        double r = rng.NextDouble()*2f-1f;
        position.y += (float) (EnemySpawnYRandomRange * r);
        Instantiate(EnemyPrefab, position, Quaternion.identity);
    }
}
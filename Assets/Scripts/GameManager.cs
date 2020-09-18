using System;
using AI;
using General;
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

    private Random rng;
    private PrefabPool enemyPool;

    public delegate void EnemySpawn(GameObject enemy);
    public event EnemySpawn OnEnemySpawn;

    public delegate void PlayerSpawn(GameObject enemy);

    public event PlayerSpawn OnPlayerSpawn;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        rng = new Random();
    }

    private void Start() {
        enemyPool = new PrefabPool(EnemyPrefab);
        GameObject player = Instantiate(PlayerPrefab, PlayerPosition, Quaternion.identity);
        OnPlayerSpawn?.Invoke(player);
        InvokeRepeating(nameof(SpawnEnemy), TimeBetweenEnemies, TimeBetweenEnemies);
    }
    
    private void SpawnEnemy() {
        Vector2 position = CenterEnemyPosition;
        double r = rng.NextDouble()*2f-1f;
        position.y += (float) (EnemySpawnYRandomRange * r);
        GameObject enemy = enemyPool.Retrieve(position, Quaternion.identity);
        enemy.GetComponent<EnemyAvatar>().SourcePool = enemyPool;
        enemy.GetComponent<KillOffscreen>().SourcePool = enemyPool;
        OnEnemySpawn?.Invoke(enemy);
    }
}
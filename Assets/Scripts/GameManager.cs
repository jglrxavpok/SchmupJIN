using System;
using System.Collections.Generic;
using AI;
using Data;
using General;
using UnityEngine;
using UnityEngine.Assertions;
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
    [SerializeField] private TextAsset level;

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

    private int levelIndex;
    private List<LevelDescription> levels;
    private Level currentLevel;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        rng = new Random();
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        enemyPool = new PrefabPool(EnemyPrefab);

        levels = XmlHelpers.DeserializeDatabaseFromXML<LevelDescription>(level);
        NextLevel();
        
        GameObject player = Instantiate(PlayerPrefab, PlayerPosition, Quaternion.identity);
        OnPlayerSpawn?.Invoke(player);
    }

    private void Update() {
        currentLevel.Execute();
        if (currentLevel.IsOver()) {
            NextLevel();
            Debug.Log($"Next level: {levels[levelIndex-1].name}");
        }
    }

    private void NextLevel() {
        if (levelIndex >= levels.Count) {
            // TODO
            throw new Exception("No more levels");
        }

        levelIndex++;
        currentLevel = new Level(enemyPool);
        currentLevel.LoadFrom(levels[levelIndex]);
    }

    public void TriggerEnemySpawnEvent(GameObject enemy) {
        OnEnemySpawn?.Invoke(enemy);
    }

}
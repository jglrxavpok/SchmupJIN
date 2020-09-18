using System.Collections.Generic;
using AI;
using General;
using UnityEngine;

namespace Data {
    public class Level {
        private readonly PrefabPool enemyPool;
        private float levelStartTime;

        private List<EnemySpawn> enemySpawns = new List<EnemySpawn>();
        private double duration;

        public Level(PrefabPool enemyPool) {
            this.enemyPool = enemyPool;
        }
        
        public void LoadFrom(LevelDescription desc) {
            duration = desc.duration;
            foreach(var enemyDesc in desc.enemies)
            {
                enemySpawns.Add(new EnemySpawn(enemyDesc));                
            }
            levelStartTime = Time.time;
        }

        public void Execute() {
            foreach (var spawn in enemySpawns) {
                if (!spawn.IsAlreadySpawned && spawn.NeedsToBeSpawned(levelStartTime)) {
                    spawn.Spawn(enemyPool);
                }
            }
        }

        private class EnemySpawn {

            private float spawnTime;
            private Vector2 spawnPosition;
            // TODO: different enemy types
            
            public EnemySpawn(EnemyDescription desc) {
                spawnTime = desc.spawnDate;
                spawnPosition = desc.spawnPosition;
            }
            
            public bool IsAlreadySpawned { get; set; }

            public bool NeedsToBeSpawned(float levelStartTime) {
                return Time.time - levelStartTime >= spawnTime;
            }

            public void Spawn(PrefabPool enemyPool) {
                IsAlreadySpawned = true;
                GameObject enemy = enemyPool.Retrieve(spawnPosition, Quaternion.identity);
                enemy.GetComponent<EnemyAvatar>().SourcePool = enemyPool;
                enemy.GetComponent<KillOffscreen>().SourcePool = enemyPool;
                GameManager.Instance.TriggerEnemySpawnEvent(enemy);
            }
            
        }

        public bool IsOver() {
            return Time.time - levelStartTime >= duration;
        }
    }
}
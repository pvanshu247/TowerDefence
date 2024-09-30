using System.Collections;
using TowerDefence.Manager;
using UnityEngine;

namespace TowerDefence.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {

        [Header("Enemy Spawn Details")]
        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private int enemyCount = 5;
        [SerializeField] private float spawnTime = 2f;
        [SerializeField] private float timeBetweenWaves = 5f;
        [SerializeField] private float difficultyFactor = 0.5f;

        private int _currentWaveIndex = 1;
        private float _timeSinceLastSpawn;
        private int _aliveEnemies;
        private int _enemiesLeftToSpawn;
        private bool _isSpawning = false;

        void OnEnable()
        {
            EnemyMovement.OnEnemyKilled += DestroyEnemy;
        }

        void Start()
        {
            StartCoroutine(StartWave());
        }

        IEnumerator StartWave()
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            _isSpawning = true;
            _enemiesLeftToSpawn = SetDifficultyFactor();
        }

        void Update()
        {
            if (!_isSpawning) return;
            _timeSinceLastSpawn += Time.deltaTime;
            if (_timeSinceLastSpawn >= spawnTime && _enemiesLeftToSpawn > 0)
            {
                SpawnEnemy();
                _timeSinceLastSpawn = 0f;
            }
            if (_aliveEnemies == 0 && _enemiesLeftToSpawn == 0)
            {
                EndWave();
            }
        }

        void EndWave()
        {
            _isSpawning = false;
            _enemiesLeftToSpawn = 0;
            _currentWaveIndex++;
            StartCoroutine(StartWave());
        }

        private void SpawnEnemy()
        {
            int index = Random.Range(0, enemyPrefabs.Length);
            GameObject enemy = Instantiate(enemyPrefabs[index], LevelManager.Instance.startPoint.position, Quaternion.identity);
            _aliveEnemies++;
            _enemiesLeftToSpawn--;
        }

        void DestroyEnemy()
        {
            _aliveEnemies--;
        }

        int SetDifficultyFactor()
        {
            return Mathf.RoundToInt(enemyCount * Mathf.Pow(_currentWaveIndex, difficultyFactor));
        }

        void OnDisable()
        {
            EnemyMovement.OnEnemyKilled -= DestroyEnemy;
        }
    }
}
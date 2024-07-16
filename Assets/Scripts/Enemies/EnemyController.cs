using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int[] enemyTypeProbabilities = { 40, 30, 20, 10 }; // Adjust probabilities as needed

    private List<GameObject> activeEnemies = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (activeEnemies.Count < maxEnemies)
            {
                int randomNumber = Random.Range(0, 100);
                int selectedEnemyIndex = 0;

                // Determine the enemy type based on probability
                for (int i = 0; i < enemyTypeProbabilities.Length; i++)
                {
                    if (randomNumber < enemyTypeProbabilities[i])
                    {
                        selectedEnemyIndex = i;
                        break;
                    }
                    randomNumber -= enemyTypeProbabilities[i];
                }

                int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
                GameObject enemyPrefab = enemyPrefabs[selectedEnemyIndex];
                Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                activeEnemies.Add(newEnemy);
                newEnemy.GetComponent<Enemy>().OnDeath += OnEnemyDeath; 
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    
    private void OnEnemyDeath(Enemy enemy)
    {
        activeEnemies.Remove(enemy.gameObject);
        enemy.OnDeath -= OnEnemyDeath;
    }
}
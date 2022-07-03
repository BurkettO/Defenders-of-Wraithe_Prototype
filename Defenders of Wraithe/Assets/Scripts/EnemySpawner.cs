using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    private int currentWave;
    [SerializeField] private PathFollower enemyPrefab;

    private LevelData levelData;

    public Path path;
    public int enemiesKilledInWave;
    private int enemiesToKillInWave;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(WaveStart(0));
    }

    private void Update()
    {
        if (enemiesKilledInWave == enemiesToKillInWave)
        {
            enemiesToKillInWave = 0;
            enemiesKilledInWave = 0;

            currentWave += 1;

            StartCoroutine(WaveStart(currentWave));
        }
    }

    private IEnumerator WaveStart(int wave)
    {
        if (wave < levelData.waveData.Length)
        {
            enemiesToKillInWave = levelData.waveData[wave].enemies.Count;

            for (int i = 0; i < levelData.waveData[wave].enemies.Count; i++)
            {
                EnemyHealth enemy = Instantiate(enemyPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<EnemyHealth>();
                enemy.gameObject.SetActive(true);
                enemy.SetHealth(levelData.waveData[wave].enemies[i].health);
                yield return new WaitForSecondsRealtime(levelData.waveData[wave].enemies[i].spawnDelay);
            }
        }

        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    [Header("UI Info")]
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI waveText;

    private int currentWave;

    [Header("Miscelaneous")]
    [SerializeField] private PathFollower enemyPrefab;

    private LevelData levelData;

    public Path path;

    public int enemiesKilledInWave;
    public int enemiesEscapedInWave;
    private int enemiesToKillInWave;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        levelData = path.levelData;
        UpdateUI();
        StartCoroutine(WaveStart(0));
    }

    private void Update()
    {
        if (enemiesKilledInWave + enemiesEscapedInWave == enemiesToKillInWave)
        {
            enemiesToKillInWave = 0;
            enemiesKilledInWave = 0;
            enemiesEscapedInWave = 0;

            currentWave += 1;

            StartCoroutine(WaveStart(currentWave));
        }
    }

    private IEnumerator WaveStart(int wave)
    {
        if (wave < levelData.waveData.Length)
        {
            enemiesToKillInWave = levelData.waveData[wave].enemies.Count;

            yield return new WaitForSecondsRealtime(levelData.waveData[wave].timerBetweenRounds);
            UpdateUI();

            for (int i = 0; i < levelData.waveData[wave].enemies.Count; i++)
            {
                EnemyHealth enemy = Instantiate(enemyPrefab.gameObject, transform.position, Quaternion.identity).GetComponent<EnemyHealth>();
                enemy.gameObject.SetActive(true);
                enemy.SetHealth(levelData.waveData[wave].enemies[i].health);
                enemy.SetValue(levelData.waveData[wave].enemies[i].value);
                enemy.GetComponent<PathFollower>().SetValues(levelData.waveData[wave].enemies[i].enemySpeed);
                yield return new WaitForSecondsRealtime(levelData.waveData[wave].enemies[i].spawnDelay);
            }
        }       
    }

    public void UpdateUI()
    {
        progressBar.fillAmount = (float)enemiesKilledInWave / (float)enemiesToKillInWave;
        progressText.text = $"{(((float)enemiesKilledInWave / (float)enemiesToKillInWave) * 100).ToString("F1")}%";
        waveText.text = $"Wave: {currentWave + 1}";
    }
}
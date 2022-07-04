using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Level Data", fileName = "new Level Data")]
public class LevelData : ScriptableObject
{
    public PathContainer pathContainer;
    public Tilemap tilemapData;

    [NonReorderable] public WaveDataContainer[] waveData;

    [System.Serializable]
    public struct PathContainer
    {
        public Vector2[] anchorPoints;        
    }

    [System.Serializable]
    public struct WaveDataContainer
    {
        [NonReorderable] public List<EnemyContainer> enemies;
        public float timerBetweenRounds;
    }

    [System.Serializable]
    public struct EnemyContainer
    {
        public float health;
        public float enemySpeed;
        public float spawnDelay;
        public int value;
    }
}
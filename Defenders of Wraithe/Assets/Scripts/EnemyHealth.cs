using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Damageable
{
    public void SetHealth(float value)
    {
        healthMax = value;
        healthCur = value;
        hasInit = true;
    }

    private void OnDisable()
    {
        EnemySpawner.Instance.enemiesKilledInWave += 1;
    }
}
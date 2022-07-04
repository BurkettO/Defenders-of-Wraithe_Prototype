using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Damageable
{
    private Animator anim;

    private int value;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        anim.Play("Damaged");
    }

    public void SetHealth(float value)
    {
        healthMax = value;
        healthCur = value;
        hasInit = true;
    }

    public void SetValue(int value_)
    {
        value = value_;
    }

    private void OnDisable()
    {
        if (healthCur <= 0) 
        { 
            EnemySpawner.Instance.enemiesKilledInWave += 1;
            Player.Instance.UpdateCurrency(value);
        }

        else { EnemySpawner.Instance.enemiesEscapedInWave += 1; }

        EnemySpawner.Instance.UpdateUI();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float healthCur;// { get; set; }
    public float healthMax;

    protected bool hasInit;

    public void Start()
    {
        if (hasInit == false)
        {
            healthCur = healthMax;
            hasInit = true;
        }  
    }

    public virtual void TakeDamage(float damage)
    {
        healthCur -= damage;

        if (healthCur <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public float ReturnHealth()
    {
        return healthCur;
    }
}

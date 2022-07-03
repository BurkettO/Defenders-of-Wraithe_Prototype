using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    public float damage;
    public float bps;
    public float range;

    public Damageable target;

    public List<Damageable> targetsInRange;

    private CircleCollider2D circleCollider;
    
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer turretSprite;
    [SerializeField] private CircleCollider2D placeRadius;

    [SerializeField] private LayerMask targetLayerMask;

    private float cd;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        targetsInRange = new List<Damageable>();
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        cd -= Time.deltaTime;

        if (target != null)
        {
            Vector2 direction = turretSprite.transform.position - target.transform.position;
            float dir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            turretSprite.transform.rotation = Quaternion.Lerp(turretSprite.transform.rotation, Quaternion.Euler(0, 0, dir - 90), 90 * Time.deltaTime);

            if (cd <= 0)
            {
                Shoot();
            }
        }

        else
        {
            if (targetsInRange.Count > 0) target = GetClosestTarget();
        }
    }

    private void Shoot()
    {
        cd = 1 / bps;
        anim.Play("MuzzleFlash");

        //RaycastHit2D hit = Physics2D.Raycast(turretSprite.transform.position, -turretSprite.transform.up, Mathf.Infinity, targetLayerMask);
        //Damageable target = hit.transform?.GetComponent<Damageable>();

        if (target != null)
        {
            target.TakeDamage(damage);

            if (target == null)
            {
                if (targetsInRange.Count > 0) target = GetClosestTarget();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            targetsInRange.Add(damageable);

            if (target == null)
            {
                target = damageable;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            if (target == damageable)
            {
                target = null;
            }

            targetsInRange.Remove(damageable);
        }
    }

    private Damageable GetClosestTarget()
    {
        Damageable closestTarget = null;
        float closestDistance = 0;

        foreach (var target in targetsInRange)
        {
            if (closestTarget == null)
            {
                closestTarget = target;
                closestDistance = Vector2.Distance(transform.position, target.transform.position); 
            }

            else
            {
                float distance = Vector2.Distance(transform.position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = target;
                    closestDistance = distance;
                }
            }
        }

        return closestTarget;
    }

    public void Setup(TurretButtons.TurretDataContainer turretData)
    {
        turretSprite.sprite = turretData.sprite;
        damage = turretData.damage;
        placeRadius.radius = turretData.placeRadius * 0.9f * 1.2f;
        range = turretData.placeRadius;
        circleCollider.radius = turretData.targetRadius * 0.9f * 1.2f;
        bps = turretData.rpm / 60;
    }
}
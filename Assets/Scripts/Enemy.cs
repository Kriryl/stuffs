using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moneyOnHit = 1f;
    [SerializeField] private float moneyOnKill = 10f;

    EnemyHealth enemyHealth;
    Money money;

    public float MoneyOnKill { get => moneyOnKill; set => moneyOnKill = value; }
    public float MoneyOnHit { get => moneyOnHit; set => moneyOnHit = value; }

    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        money = FindObjectOfType<Money>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponentInParent<Tower>())
        {
            money.Add(MoneyOnHit);
            ApplyDamage(other);
            KillIfDead(other);
        }
    }

    private void ApplyDamage(GameObject other)
    {
        float damage = other.GetComponentInParent<Tower>().Damage;
        enemyHealth.LoseHealth(damage);
        other.GetComponentInParent<Tower>().DamageDone += damage;
    }

    private void KillIfDead(GameObject other)
    {
        bool isDead = enemyHealth.IsDead();
        if (isDead)
        {
            other.GetComponentInParent<Tower>().Kills += 1;
            money.Add(MoneyOnKill);
        }
        enemyHealth.Kill();
    }
}

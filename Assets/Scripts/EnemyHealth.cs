using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider healthSlider = null;

    [SerializeField] private float health = 10f;
    [SerializeField] private float maxHealth = 20f;
    [SerializeField] private float healthIncrease = 4f;
    [SerializeField] private float speedIncrease = 0f;

    [SerializeField] private bool randomHealth = false;
    [Header("Randomize health")]
    [SerializeField] private float minHealthMultiplier = 0.5f;
    [SerializeField] private float maxHealthMultiplier = 1.5f;

    private float startHealth;
    private bool maxHealthChangable = true;

    Enemy enemy;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        if (maxHealthChangable)
        {
            startHealth = maxHealth;
            maxHealthChangable = false;
        }
        print(startHealth);
    }

    private void OnEnable()
    {
        maxHealth = startHealth;
        if (randomHealth)
        {
            float randomMultiplier = Random.Range(minHealthMultiplier, maxHealthMultiplier);
            print(Mathf.Round(randomMultiplier * 10) / 10);
            maxHealth *= Mathf.Round(randomMultiplier * 10f) / 10f;
        }
        if (healthSlider)
        {
            healthSlider.maxValue = maxHealth;
        }
        health = maxHealth;
    }

    private void Update()
    {
        if (health < 0f)
        {
            health = 0f;
        }
        DisplayCurrentHealth();
    }

    public void LoseHealth(float amount)
    {
        GameObject damageText = DamageText.Create(amount, transform.position);
        damageText.GetComponent<DamageText>().DestroyText(1f);
        health -= amount;
    }

    public void Kill()
    {
        if (health <= 0)
        {
            startHealth += healthIncrease;
            EnemyMover enemyMover = GetComponent<EnemyMover>();
            if (enemyMover)
            {
                enemyMover.Speed += speedIncrease;
            }
            gameObject.SetActive(false);
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void DisplayCurrentHealth()
    {
        if (healthSlider)
        {
            healthSlider.value = health;
        }
    }
}

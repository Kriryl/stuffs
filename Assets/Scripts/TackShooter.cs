using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackShooter : MonoBehaviour
{
    // Tack Shooter is a short range attack tower, with many projectiles shooting outwards itself
    [SerializeField] ParticleSystem[] tacks;
    [SerializeField] float range = 10f;

    private enum State { deactive, active}
    private State currentState;

    private Enemy[] enemiesInRange;

    private void Update()
    {
        FindEnemiesInRange();
        CheckState();
    }

    private void CheckState()
    {
        bool isActive = currentState == State.active;
        ActivateTacks(isActive);
    }

    private void ActivateTacks(bool value)
    {
        foreach (ParticleSystem tack in tacks)
        {
            var emission = tack.emission;
            emission.enabled = value;
        }
    }

    // Range of the tack shooter 
    private void FindEnemiesInRange()
    {
        enemiesInRange = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemiesInRange)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (enemyDistance <= range && enemy.gameObject.activeInHierarchy)
            {
                currentState = State.active;
            }
            else
            {
                currentState = State.deactive;
            }
        }
    }
}

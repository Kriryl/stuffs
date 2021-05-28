using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Ballista : MonoBehaviour
{
    // Ballista is an attack tower with unlimited range
    [SerializeField] GameObject ballistaTop = null;
    [SerializeField] private ParticleSystem ballistaArrow;

    private enum State { idle, target }
    State currentState;

    Enemy[] enemies;
    Transform target;

    private void Update()
    {
        if (!target || target.gameObject.activeInHierarchy == false)
        {
            FindClosestEnemy();
        }
        CheckState();
        ToggleState();
    }

    private void ToggleState()
    {
        if (currentState == State.target)
        {
            TargetEnemy();
        }
        else if (currentState == State.idle)
        {
            Idle();
        }
    }

    private void FindClosestEnemy()
    {
        enemies = FindObjectsOfType<Enemy>();
        Transform closestEnemy = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (enemyDistance < maxDistance)
            {
                closestEnemy = enemy.transform;
                maxDistance = enemyDistance;
            }
        }
        target = closestEnemy;
    }

    private void CheckState()
    {
        if (target != null)
        {
            currentState = State.target;
        }
        else
        {
            currentState = State.idle;
        }
    }

    private void TargetEnemy()
    {
        if (ballistaTop && target)
        {
            ballistaTop.transform.LookAt(target.transform);
            if (ballistaArrow)
            {
                var arrowEmmision = ballistaArrow.emission;
                arrowEmmision.enabled = true;
            }
        }
    }

    private void Idle()
    {
        if (ballistaArrow)
        {
            var arrowEmmision = ballistaArrow.emission;
            arrowEmmision.enabled = false;
        }
    }
}

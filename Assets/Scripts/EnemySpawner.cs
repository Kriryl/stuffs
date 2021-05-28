using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] pool;
    [SerializeField] int poolSize = 4;
    [SerializeField] GameObject enemy = null;
    [SerializeField] float wait = 1;
    [SerializeField] float waitDecrease = 0.99f;
    [SerializeField] private bool stopSpawning;

    float startingWait;

    private void Start()
    {
        startingWait = wait;
        PopulatePool();
        StartCoroutine(StartSpawning());
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, transform.position, transform.rotation);
            pool[i].transform.parent = transform;
            pool[i].SetActive(false);
        }
    }

    private IEnumerator StartSpawning()
    {
        while (!stopSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(wait);
            bool stopDecreasingWait = wait <= (startingWait / 2);
            if (!stopDecreasingWait)
            {
                wait *= waitDecrease;
            }
        }
    }

    private void SpawnEnemy()
    {
        foreach (GameObject ram in pool)
        {
            if (!ram.activeInHierarchy)
            {
                ram.SetActive(true);
                return;
            }
        }
    }

    //[SerializeField] private List<Round> rounds;

    //[SerializeField] GameObject ram = null;
    //[SerializeField] float wait = 4f;
    //[SerializeField] float duration = 5f;

    //[SerializeField] int poolSize = 4;

    //bool stopSpawning = false;
    //[SerializeField]
    //private List<GameObject> pool;

    //private void Start()
    //{
    //    ProceedRounds();
    //}

    //private void ProceedRounds()
    //{
    //    StartCoroutine(ApplyStats());
    //}

    //private IEnumerator ApplyStats()
    //{
    //    foreach (Round round in rounds)
    //    {
    //        print("starting new round");
    //        wait = round.WaitTime;
    //        poolSize = round.NumOfEnemies;
    //        duration = round.Duration;
    //        print(round.name);
    //        StartRound();
    //        yield return new WaitForSeconds(duration);
    //        print("Round ended.");
    //        stopSpawning = true;
    //    }
    //}

    //private void StartRound()
    //{
    //    ClearEnemies();
    //    for (int i = 0; i < poolSize; i++)
    //    {
    //        GameObject enemy = Instantiate(ram, transform.position, transform.rotation);
    //        enemy.transform.parent = transform;
    //        pool.Add(enemy);
    //        enemy.SetActive(false);
    //    }

    //    print("spawning enemy");
    //    StartCoroutine(SpawnEnemy());
    //}

    //private void ClearEnemies()
    //{
    //    foreach (GameObject ram in pool)
    //    {
    //        if (!ram.activeInHierarchy)
    //        {
    //            Destroy(ram);
    //        }
    //    }
    //    pool.Clear();
    //}

    //private IEnumerator SpawnEnemy()
    //{
    //    while (!stopSpawning)
    //    {
    //        foreach (GameObject ram in pool.ToArray())
    //        {
    //            if (!ram.activeInHierarchy)
    //            {
    //                ram.SetActive(true);
    //            }
    //            yield return new WaitForSeconds(wait);
    //        }
    //    }
    //}
}

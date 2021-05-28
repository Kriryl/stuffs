using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab = null;
    [SerializeField] private bool isDefaultTower = false;

    private TowerPlacer towerPlacer;

    private void Start()
    {
        towerPlacer = FindObjectOfType<TowerPlacer>();
        if (isDefaultTower)
        {
            towerPlacer.SetTower(null);
            if (towerPrefab)
            {
                towerPlacer.SetTower(towerPrefab);
            }
        }
    }

    public void Select()
    {
        towerPlacer.SetTower(null);
        if (towerPrefab)
        {
            towerPlacer.SetTower(towerPrefab);
        }
    }
}

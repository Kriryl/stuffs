using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    private Tower selectedTower;

    public void SetTower(Tower tower)
    {
        selectedTower = tower;
    }

    public Tower ReturnSelectedTower()
    {
        return selectedTower;
    }
}

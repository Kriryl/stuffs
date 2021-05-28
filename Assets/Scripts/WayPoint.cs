using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = true;
    [SerializeField] bool isPath = false;

    Money money;
    TowerPlacer towerPlacer;
    Lives lives;

    private bool closedByTower = false;

    public bool IsPlaceable => isPlaceable;

    public bool IsPath => isPath;

    private void Start()
    {
        money = FindObjectOfType<Money>();
        towerPlacer = FindObjectOfType<TowerPlacer>();
        lives = FindObjectOfType<Lives>();
    }

    private void OnMouseDown()
    {
        if (isPlaceable && !lives.GameIsOver)
        {
            CheckMoney();
        }
        else if (closedByTower)
        {
            SellTower();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (closedByTower)
            {
                Tower tower = GetComponentInChildren<Tower>();
                if (tower)
                {
                    float cost1 = tower.Upgrade1Cost;
                    float cost2 = tower.Upgrade2Cost;
                    float cost3 = tower.Upgrade3Cost;

                    if (!tower.U1 && cost1 <= money.Get())
                    {
                        money.Spend(cost1);
                        money.TurnGreen();
                        tower.UpgradeSlotOne();
                        return;
                    }
                    else if (!tower.U2 && cost2 <= money.Get())
                    {
                        money.Spend(cost2);
                        money.TurnGreen();
                        tower.UpgradeSlotTwo();
                        return;
                    }
                    else if (!tower.U3 && cost3 <= money.Get())
                    {
                        money.Spend(cost3);
                        money.TurnGreen();
                        tower.UpgradeSlotThree();
                        return;
                    }
                    StartCoroutine(money.TurnRed());
                    print("Not enough money to upgrade!");
                }
            }
        }
    }

    private void CheckMoney()
    {
        Tower newTower = towerPlacer.ReturnSelectedTower();
        if (newTower)
        {
            float cost = newTower.Cost;
            if (cost > money.Get())
            {
                StartCoroutine(money.TurnRed());
                print("Not enough money!");
                return;
            }
            money.Spend(cost);
            money.TurnGreen();
            PlaceTower(newTower);
        }
    }

    private void PlaceTower(Tower tower)
    {
        GameObject newTower = Instantiate(tower.gameObject, transform.position, transform.rotation);
        newTower.transform.parent = transform;
        isPlaceable = false;
        closedByTower = true;
    }

    private void SellTower()
    {
        int numOfChildren = transform.childCount;
        if (numOfChildren > 0)
        {
            Tower tower = GetComponentInChildren<Tower>();
            if (tower)
            {
                float towerSellValue = tower.SellCost;
                money.Add(towerSellValue);
                Destroy(tower.gameObject);
                isPlaceable = true;
                closedByTower = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    [SerializeField] private float upgrade1Cost = 200f;
    [SerializeField] private float upgrade2Cost = 500f;
    [SerializeField] private float upgrade3Cost = 1100f;


    public float GetUpgrade1Cost()
    {
        return upgrade1Cost;
    }

    public float GetUpgrade2Cost()
    {
        return upgrade2Cost;
    }

    public float GetUpgrade3Cost()
    {
        return upgrade3Cost;
    }
}

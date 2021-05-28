using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float cost = 10f;
    [SerializeField] private int kills = 0;
    [SerializeField] private float damageDone = 0f;

    [SerializeField] private TextMeshProUGUI upgradeDisplay = null;

    ParticleSystem[] particleSystems;
    Upgrade upgrade;
    float upgrade1Cost;
    float upgrade2Cost;
    float upgrade3Cost;

    public float DamageDone { get { return damageDone; } set { damageDone = value; } }

    public bool U1 { get; private set; }

    public bool U2 { get; private set; }

    public bool U3 { get; private set; }

    public float SellCost { get; set; } = 0f;

    public float Damage => damage;

    public float Cost => cost;

    public int Kills { get { return kills; } set { kills = value; } }

    public float Upgrade1Cost => upgrade1Cost;
    public float Upgrade2Cost => upgrade2Cost;
    public float Upgrade3Cost => upgrade3Cost;

    private void Start()
    {
        SellCost = cost;
        upgrade = GetComponent<Upgrade>();
        if (upgrade)
        {
            upgrade1Cost = upgrade.GetUpgrade1Cost();
            upgrade2Cost = upgrade.GetUpgrade2Cost();
            upgrade3Cost = upgrade.GetUpgrade3Cost();
        }
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        UpdateDisplay(0);
    }

    public void UpgradeSlotThree()
    {
        SellCost += Upgrade3Cost / 2f;
        damage *= 2f;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var emission = particleSystem.emission;
            var main = particleSystem.main;
            var startSpeed = main.startSpeed;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(3f);
            main.startSpeed = new ParticleSystem.MinMaxCurve(startSpeed.constant * 2f);
        }
        UpdateDisplay(3);
        U3 = true;
    }

    public void UpgradeSlotTwo()
    {
        SellCost += Upgrade2Cost / 2f;
        damage += 1f;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var main = particleSystem.main;
            var startSpeed = main.startSpeed;
            main.startSpeed = new ParticleSystem.MinMaxCurve(startSpeed.constant * 1.5f);
        }
        UpdateDisplay(2);
        U2 = true;
    }

    public void UpgradeSlotOne()
    {
        SellCost += Upgrade1Cost / 2f;
        damage += 1f;
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            var emission = particleSystem.emission;
            var rOF = emission.rateOverTime;
            emission.rateOverTime = new ParticleSystem.MinMaxCurve(rOF.constant * 1.5f);
            UpdateDisplay(1);
            U1 = true;
        }
    }

    private void UpdateDisplay(int upgrade)
    {
        if (upgradeDisplay)
        {
            if (upgrade <= 0)
            {
                upgradeDisplay.text = "";
            }
            else
            {
                upgradeDisplay.text = upgrade.ToString();
            }
        }
    }
}

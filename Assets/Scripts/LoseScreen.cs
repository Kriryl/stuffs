using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    private static float totalDamage = 0f;
    private static int totalKills = 0;

    public static void CreateSummary()
    {
        GetTowerInfo();
    }

    private static void GetTowerInfo()
    {
        totalDamage = 0f;
        totalKills = 0;
        Tower[] towers = FindObjectsOfType<Tower>();
        foreach (Tower tower in towers)
        {
            totalDamage += tower.DamageDone;
            totalKills += tower.Kills;
        }
    }

    public static float GetTotalDamage()
    {
        if (totalDamage > PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_DAMAGE))
        {
            ScoreSaver.SetDamage(totalDamage);
        }
        return totalDamage;
    }

    public static float GetTotalKills()
    {
        if (totalKills > PlayerPrefs.GetInt(ScoreSaver.HIGH_SCORE_KILLS))
        {
            ScoreSaver.SetKills(totalKills);
        }
        return totalKills;
    }
}

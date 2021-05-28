using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    public const string HIGH_SCORE_TIME_ALIVE = "Time Alive";
    public const string HIGH_SCORE_DAMAGE = "Total Damage";
    public const string HIGH_SCORE_KILLS = "Total Kills";

    public static void SetTimeAlive(float secs)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_TIME_ALIVE, secs);
    }

    public static void SetDamage(float damage)
    {
        PlayerPrefs.SetFloat(HIGH_SCORE_DAMAGE, damage);
    }

    public static void SetKills(int kills)
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KILLS, kills);
    }
}

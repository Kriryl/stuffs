using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScoreDisplayer : MonoBehaviour
{
    private enum ScoreType { bestKills, bestDamage, bestTimeAlive}

    [SerializeField] private ScoreType scoreType;
    TextMeshProUGUI textMesh;


    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        if (textMesh)
        {
            DisplayScore();
        }
    }

    private void DisplayScore()
    {
        if (scoreType == ScoreType.bestDamage)
        {
            textMesh.text = $"Most damage dealt: {PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_DAMAGE)}";
        }
        else if (scoreType == ScoreType.bestKills)
        {
            textMesh.text = $"Most kills: {PlayerPrefs.GetInt(ScoreSaver.HIGH_SCORE_KILLS)}";
        }
        else if (scoreType == ScoreType.bestTimeAlive)
        {
            textMesh.text = $"Best time: {Mathf.Round(PlayerPrefs.GetFloat(ScoreSaver.HIGH_SCORE_TIME_ALIVE) * 10f / 10f)} seconds";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    [SerializeField] private float money = 0f;
    [SerializeField] private GameObject moneyDisplay;
    [SerializeField] private float redInSeconds = 2f;

    TextMeshProUGUI moneyText;
    Color moneyStartingColor;

    bool isRed;

    private void Start()
    {
        moneyText = moneyDisplay.GetComponent<TextMeshProUGUI>();
        moneyStartingColor = moneyText.color;
        DisplayMoney();
    }

    public float Get()
    {
        return money;
    }

    public void Add(float amount)
    {
        money += amount;
        DisplayMoney();
    }

    public void Spend(float amount)
    {
        money -= amount;
        DisplayMoney();
    }

    public IEnumerator TurnRed()
    {
        if (!isRed)
        {
            moneyText.color = Color.red;
            isRed = true;
            yield return new WaitForSeconds(redInSeconds);
            moneyText.color = moneyStartingColor;
            isRed = false;
        }
        else
        {
            yield return new WaitForEndOfFrame();
        }
    }

    public void TurnGreen()
    {
        moneyText.color = moneyStartingColor;
    }

    private void DisplayMoney()
    {
        if (moneyText)
        {
            moneyText.text = $"${Mathf.Round(money)}";
        }
    }
}

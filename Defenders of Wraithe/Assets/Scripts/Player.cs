using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public int currencyMax;
    public int currencyCur { private set; get; }

    public int healthMax;
    public int healthCur { private set; get; }

    public int turretCapMax;
    public int turretCapCur { private set; get; }

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI currencyText;
    [SerializeField] private TextMeshProUGUI turretCapText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        currencyCur = 165;
        healthCur = healthMax;
        turretCapCur = 0;

        healthText.text = $"{healthCur}";
        currencyText.text = $"{currencyCur}";
        turretCapText.text = $"{turretCapCur} / {turretCapMax}";
    }

    public void UpdateUI()
    {
        healthText.text = $"{healthCur}";
        currencyText.text = $"{currencyCur}";
        turretCapText.text = $"{turretCapCur} / {turretCapMax}";
    }

    public void UpdateHealth(int value)
    {
        healthCur += value;
        UpdateUI();
    }

    public void UpdateCurrency(int value)
    {
        currencyCur += value;
        UpdateUI();
    }

    public void UpdateTurretCapacity(int value)
    {
        turretCapCur += value;
        UpdateUI();
    }
}
using TMPro;
using UnityEngine;

public class Income : MonoBehaviour
{
    public int Cash { get => cash; private set => cash = value; }

    [SerializeField] private int startCash = 200;
    [SerializeField] private int incomeIntervalInSeconds = 5;
    [SerializeField] private int incomePerInterval = 50;
    [SerializeField] private int incomePerKill = 10;

    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private IncomeFeedback incomeFeedback;
    [SerializeField] private TextMeshProUGUI killText;

    // Cache
    private int cash;
    private float timePassedOfInterval;
    private int killCounter = 0;

    private Config config;


    public void Subtract(int amount)
    {
        Cash -= amount;
        UpdateCashText();
        incomeFeedback.DisplayIncomeUpdate(-amount);
    }

    public void Add(int amount)
    {
        Cash += amount;
        UpdateCashText();
        incomeFeedback.DisplayIncomeUpdate(amount);
    }

    public void KilledEnemy()
    {
        Add(incomePerKill);
        killCounter++;
        UpdateKillText();
    }

    private void UpdateKillText()
    {
        killText.text = "Kills: " + killCounter.ToString();
    }

    private void Awake()
    {
        config = FindObjectOfType<Config>();
        Cash = startCash;
        UpdateCashText();
        UpdateCashTextColor(config.TextColor);
        killCounter = 0;
        UpdateKillText();
    }

    public void OnUpdate()
    {
        timePassedOfInterval += Time.deltaTime;
        bool exceededInterval = timePassedOfInterval > incomeIntervalInSeconds;
        if (!exceededInterval)
        {
            return;
        }

        timePassedOfInterval = 0;
        AddIncomePerInterval();
    }

    private void AddIncomePerInterval()
    {
        Add(incomePerInterval);
    }

    private void UpdateCashText()
    {
        cashText.text = "$ " + cash.ToString();
    }

    private void UpdateCashTextColor(Color color)
    {
        cashText.color = color;
    }
}

using TMPro;
using UnityEngine;

public class Income : MonoBehaviour
{
    public int Cash { get => cash; private set => cash = value; }

    [SerializeField] private int startCash = 200;
    [SerializeField] private int incomeIntervalInSeconds = 5;
    [SerializeField] private int incomePerInterval = 50;

    [SerializeField] private TextMeshProUGUI cashText;
    [SerializeField] private IncomeFeedback incomeFeedback;

    // Cache
    private int cash;
    private float timePassedOfInterval;

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


    private void Awake()
    {
        config = FindObjectOfType<Config>();
        Cash = startCash;
        UpdateCashText();
        UpdateCashTextColor(config.TextColor);
    }

    private void Update()
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

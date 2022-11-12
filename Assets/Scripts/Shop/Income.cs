using TMPro;
using UnityEngine;

public class Income : MonoBehaviour
{
    public int Cash { get => cash; private set => cash = value; }

    [SerializeField] private int startCash = 200;
    [SerializeField] private int incomeIntervalInSeconds = 5;
    [SerializeField] private int incomePerInterval = 50;

    [SerializeField] private TextMeshProUGUI cashText;

    // Cache
    private int cash;
    private float timePassedOfInterval;

    private Config config;


    public void Subtract(int amount)
    {
        Cash -= amount;
        UpdateCashText();
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
        cash += incomePerInterval;
        UpdateCashText();
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

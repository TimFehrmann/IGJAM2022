using UnityEngine;

public class Income : MonoBehaviour
{
    private int cash;

    [SerializeField] private int startCash = 200;
    [SerializeField] private int incomeIntervalInSeconds = 5;
    [SerializeField] private int incomePerInterval = 50;

    // Cache
    private float timePassedOfInterval;

    public int Cash { get => cash; private set => cash = value; }

    public void Subtract(int amount)
    {
        Cash -= amount;
    }

    private void Awake()
    {
        Cash = startCash;
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
        Cash += incomePerInterval;

    }
}

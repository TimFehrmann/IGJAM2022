using UnityEngine;
using Assets.Scripts.Utility;

public class IncomeFeedback : MonoBehaviour
{
    [SerializeField] private FeedbackText feedbackTextPrefab;
    [SerializeField] private Transform feedbackSpawnContainer;

    // Cached
    private Config config;
    private ObjectPool<FeedbackText> feedbackTextObjectPool; 

    private void Awake()
    {
        config = FindObjectOfType<Config>();
        feedbackTextObjectPool = new ObjectPool<FeedbackText>(10, feedbackTextPrefab);
    }

    public void DisplayIncomeUpdate(int amount)
    {
        // Determine Color & Text Prefix (+/-)
        bool isPositiveAmount = amount >= 0;
        Color color = isPositiveAmount ? config.TextValidColor : config.TextInvalidColor;
        string textPrefix = isPositiveAmount ? "+" : "";
        string text = textPrefix + amount.ToString();

        SpawnFeedbackText(color, text);
    }

    private void SpawnFeedbackText(Color color, string text)
    {
        FeedbackText feedbackText = feedbackTextObjectPool.GetObject();
        feedbackText.transform.SetParent(feedbackSpawnContainer.transform);
        feedbackText.Display(feedbackTextObjectPool, color, text);
    }
}

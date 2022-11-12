using UnityEngine;
using TMPro;
using Assets.Scripts.Utility;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class FeedbackText : MonoBehaviour
{
    // Cached
    private TextMeshProUGUI feedbackText;
    private ObjectPool<FeedbackText> feedbackTextObjectPool;
    private RectTransform rectTransform;

    public void Display(ObjectPool<FeedbackText> objectPool, Color color, string text)
    {
        feedbackTextObjectPool = objectPool;

        gameObject.SetActive(true);

        // Set Position
        rectTransform.offsetMin = new Vector2(8, 8);
        rectTransform.offsetMax = new Vector2(-8, -8);

        transform.localScale = Vector3.one;

        feedbackText.color = color;
        feedbackText.text = text;
    } 

    public void Hide()
    {
        gameObject.SetActive(false);
        feedbackTextObjectPool.ReturnToPool(this);
    }

    private void Awake()
    {
        feedbackText = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }    

}

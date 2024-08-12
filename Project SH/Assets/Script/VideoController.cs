using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public ScrollRect scrollRect; // Tham chiếu tới Scroll Rect
    public RectTransform content; // Tham chiếu tới Content
    public int totalLevels = 5; // Tổng số level
    public float snapSpeed = 10f; // Tốc độ chuyển động khi vuốt

    private float[] levelPositions;
    private int currentLevel = 0;
    private bool isDragging = false;

    void Start()
    {
        int childCount = content.childCount;
        totalLevels = Mathf.Min(totalLevels, childCount);
        levelPositions = new float[totalLevels];

        for (int i = 0; i < totalLevels; i++)
        {
            levelPositions[i] = (float)i / (totalLevels - 1);
        }
    }

    void Update()
    {
        if (!isDragging)
        {
            float targetPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, levelPositions[currentLevel], snapSpeed * Time.deltaTime);
            scrollRect.horizontalNormalizedPosition = targetPosition;
        }
    }

    public void OnBeginDrag()
    {
        isDragging = true;
    }

    public void OnEndDrag()
    {
        isDragging = false;
        float nearestPosition = Mathf.Infinity;
        for (int i = 0; i < levelPositions.Length; i++)
        {
            float distance = Mathf.Abs(scrollRect.horizontalNormalizedPosition - levelPositions[i]);
            if (distance < nearestPosition)
            {
                nearestPosition = distance;
                currentLevel = i;
            }
        }
    }
}

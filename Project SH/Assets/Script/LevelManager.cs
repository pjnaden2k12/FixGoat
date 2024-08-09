using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelMenuController : MonoBehaviour
{
    public Image levelImage; // Hình ảnh của level
    public Button leftButton; // Nút trái
    public Button rightButton; // Nút phải
    public Button playButton; // Nút chơi
    public Sprite[] levelSprites; // Mảng chứa các sprite của level
    public float transitionDuration = 0.5f; // Thời gian chuyển đổi

    private int currentLevelIndex = 0;
    private bool isTransitioning = false;

    void Start()
    {
        // Thiết lập sự kiện cho các nút
        leftButton.onClick.AddListener(ShowPreviousLevel);
        rightButton.onClick.AddListener(ShowNextLevel);
        playButton.onClick.AddListener(PlayCurrentLevel);

        // Hiển thị level đầu tiên
        UpdateLevelImage();
    }

    void ShowPreviousLevel()
    {
        if (currentLevelIndex > 0 && !isTransitioning)
        {
            currentLevelIndex--;
            StartCoroutine(SmoothTransition(levelSprites[currentLevelIndex], Vector3.left));
        }
    }

    void ShowNextLevel()
    {
        if (currentLevelIndex < levelSprites.Length - 1 && !isTransitioning)
        {
            currentLevelIndex++;
            StartCoroutine(SmoothTransition(levelSprites[currentLevelIndex], Vector3.right));
        }
    }

    void UpdateLevelImage()
    {
        levelImage.sprite = levelSprites[currentLevelIndex];
    }

    void PlayCurrentLevel()
    {
        // Load scenes tương ứng với level hiện tại
        SceneManager.LoadScene("Level" + (currentLevelIndex + 1));
    }

    IEnumerator SmoothTransition(Sprite newSprite, Vector3 direction)
    {
        isTransitioning = true;

        Vector3 originalPosition = levelImage.rectTransform.localPosition;
        Vector3 offScreenPosition = originalPosition + direction * levelImage.rectTransform.rect.width;

        // Clone the levelImage and move it to offScreenPosition
        GameObject newImageObject = new GameObject("NewImage");
        Image newImage = newImageObject.AddComponent<Image>();
        newImage.sprite = newSprite;
        newImage.rectTransform.SetParent(levelImage.rectTransform.parent, false);
        newImage.rectTransform.localPosition = offScreenPosition;
        newImage.rectTransform.sizeDelta = levelImage.rectTransform.sizeDelta;

        float elapsedTime = 0;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;

            levelImage.rectTransform.localPosition = Vector3.Lerp(originalPosition, -direction * levelImage.rectTransform.rect.width, t);
            newImage.rectTransform.localPosition = Vector3.Lerp(offScreenPosition, originalPosition, t);

            yield return null;
        }

        // Finalize positions
        levelImage.rectTransform.localPosition = -direction * levelImage.rectTransform.rect.width;
        newImage.rectTransform.localPosition = originalPosition;

        // Update levelImage with the new sprite
        levelImage.sprite = newSprite;
        levelImage.rectTransform.localPosition = originalPosition;

        // Destroy the temporary newImage
        Destroy(newImageObject);

        isTransitioning = false;
    }
}

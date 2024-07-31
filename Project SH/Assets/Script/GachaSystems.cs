using UnityEngine;
using System.Collections;

public class GachaSystem : MonoBehaviour
{
    public int diamondCost = 10; // Số lượng kim cương cần để quay gacha
    public int minTowerPieces = 1; // Số lượng mảnh tháp ít nhất có thể nhận được
    public int maxTowerPieces = 5; // Số lượng mảnh tháp tối đa có thể nhận được
    public NotificationManager notificationManager; // Tham chiếu đến NotificationManager
    public RectTransform spinningImage; // Hình ảnh rung (RectTransform)

    public float shakeMagnitude = 10f; // Độ rung của hình ảnh
    public float shakeDuration = 2f; // Thời gian rung

    private void Start()
    {
        // Đảm bảo hình ảnh rung không hiển thị khi bắt đầu
        if (spinningImage != null)
        {
            spinningImage.gameObject.SetActive(false);
        }
    }

    // Hàm thực hiện quay gacha
    public void Spin()
    {
        // Bắt đầu coroutine để xử lý quay gacha với hiệu ứng rung
        StartCoroutine(SpinWithShakeEffect());
    }

    private IEnumerator SpinWithShakeEffect()
    {
        // Hiển thị hình ảnh rung
        if (spinningImage != null)
        {
            spinningImage.gameObject.SetActive(true);
        }

        // Bắt đầu hiệu ứng rung
        float elapsedTime = 0f;
        Vector3 originalPosition = spinningImage.localPosition;

        while (elapsedTime < shakeDuration)
        {
            // Tạo hiệu ứng rung bằng cách di chuyển hình ảnh ngẫu nhiên
            Vector3 shakeOffset = Random.insideUnitCircle * shakeMagnitude;
            spinningImage.localPosition = originalPosition + shakeOffset;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đặt lại vị trí của hình ảnh
        spinningImage.localPosition = originalPosition;

        // Ẩn hình ảnh rung sau khi hoàn thành
        if (spinningImage != null)
        {
            spinningImage.gameObject.SetActive(false);
        }

        // Tiến hành quay gacha sau khi hiệu ứng rung hoàn thành
        PerformGacha();
    }

    private void PerformGacha()
    {
        // Kiểm tra xem người chơi có đủ kim cương không
        if (ResourceManager.Instance.diamonds >= diamondCost)
        {
            // Trừ số kim cương từ ResourceManager
            ResourceManager.Instance.SpendDiamonds(diamondCost);

            // Tính số mảnh tháp ngẫu nhiên mà người chơi nhận được
            int piecesReceived = Random.Range(minTowerPieces, maxTowerPieces + 1);
            ResourceManager.Instance.AddTowerPieces(piecesReceived);

            // Hiện thông báo với số mảnh tháp nhận được
            if (notificationManager != null)
            {
                notificationManager.ShowNotification(piecesReceived);
            }
        }
        else
        {
            // Hiện thông báo không đủ kim cương
            Debug.Log("Not enough diamonds to spin the gacha.");
        }
    }
}

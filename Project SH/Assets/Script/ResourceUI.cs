using UnityEngine;
using UnityEngine.UI;
using TMPro; // Thêm namespace cho TextMeshPro

public class ResourceUIManager : MonoBehaviour
{
    public Image avatarImage; // Hình ảnh avatar nhân vật
    public Image goldIcon; // Hình ảnh biểu tượng vàng
    public TMP_Text goldText; // Thay Text bằng TMP_Text
    public Image diamondsIcon; // Hình ảnh biểu tượng kim cương
    public TMP_Text diamondsText; // Thay Text bằng TMP_Text
    public Image towerPiecesIcon; // Hình ảnh biểu tượng mảnh tháp
    public TMP_Text towerPiecesText; // Thay Text bằng TMP_Text

    public Sprite defaultAvatarSprite; // Sprite cho avatar mặc định
    public Sprite goldSprite; // Sprite cho vàng
    public Sprite diamondsSprite; // Sprite cho kim cương
    public Sprite towerPiecesSprite; // Sprite cho mảnh tháp

    private void Awake()
    {
        // Đảm bảo ResourceUI không bị hủy khi đổi scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Đặt hình ảnh cho các tài nguyên
        goldIcon.sprite = goldSprite;
        diamondsIcon.sprite = diamondsSprite;
        towerPiecesIcon.sprite = towerPiecesSprite;

        // Đặt hình ảnh cho avatar
        avatarImage.sprite = defaultAvatarSprite;

        // Cập nhật UI với giá trị ban đầu
        UpdateResourceUI();
    }

    private void OnEnable()
    {
        // Đăng ký sự kiện hoặc kiểm tra sự thay đổi tài nguyên
        ResourceManager.Instance.OnResourceChanged += UpdateResourceUI;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi không cần thiết
        ResourceManager.Instance.OnResourceChanged -= UpdateResourceUI;
    }

    public void UpdateResourceUI()
    {
        // Cập nhật số lượng tài nguyên
        goldText.text = ResourceManager.Instance.gold.ToString();
        diamondsText.text = ResourceManager.Instance.diamonds.ToString();
        towerPiecesText.text = ResourceManager.Instance.towerPieces.ToString();
    }

    public void UpdateAvatar(Sprite newAvatarSprite)
    {
        avatarImage.sprite = newAvatarSprite;
    }
}

using UnityEngine;

public class TowerManagerInGame : MonoBehaviour
{
    public GameObject[] towerSlots;  // Các ô chứa tháp
    public GameObject buyTowerPanel; // Panel mua tháp
    public GameObject upgradeTowerPanel; // Panel nâng cấp tháp
    public TowerDisplayManager towerDisplayManager;

    public static TowerManagerInGame Instance; // Để truy cập từ TowerDisplayManager

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Ẩn các Panel khi bắt đầu
        buyTowerPanel.SetActive(false);
        upgradeTowerPanel.SetActive(false);
    }

    void Update()
    {
        // Kiểm tra sự kiện nhấp chuột
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

            if (hit != null)
            {
                // Xác định ô tháp nào đã được nhấp
                for (int i = 0; i < towerSlots.Length; i++)
                {
                    if (hit.gameObject == towerSlots[i])
                    {
                        OnSlotClicked(i);
                        break;
                    }
                }
            }
        }
    }

    void OnSlotClicked(int index)
    {
        if (towerSlots[index].transform.childCount == 0)
        {
            // Nếu ô chưa có tháp, hiển thị panel mua tháp
            buyTowerPanel.SetActive(true);
            upgradeTowerPanel.SetActive(false);

            // Truyền chỉ số của ô tháp vào TowerDisplayManager
            towerDisplayManager.SetSelectedSlotIndex(index);
        }
        else
        {
            // Nếu ô đã có tháp, hiển thị panel nâng cấp tháp
            buyTowerPanel.SetActive(false);
            upgradeTowerPanel.SetActive(true);
        }
    }
}

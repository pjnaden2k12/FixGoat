//using UnityEngine;
//using DG.Tweening; // Đảm bảo bạn đã thêm DOTween vào dự án của mình
//using UnityEngine.UI;
//public class TowerManagerInGame : MonoBehaviour
//{
//    public GameObject[] towerSlots;  // Các ô chứa tháp
//    public GameObject buyTowerPanel; // Panel mua tháp
//    public GameObject upgradeTowerPanel; // Panel nâng cấp tháp
//    public TowerDisplayManager towerDisplayManager;
//    public GameObject closeBuyPanelButton; // Nút đóng panel mua tháp
//    public GameObject closeUpgradePanelButton; // Nút đóng panel nâng cấp tháp

//    public static TowerManagerInGame Instance; // Để truy cập từ TowerDisplayManager

//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Start()
//    {
//        // Ẩn các Panel khi bắt đầu và đặt kích thước của panel về kích thước bình thường
//        buyTowerPanel.SetActive(false);
//        upgradeTowerPanel.SetActive(false);
//        SetPanelScale(buyTowerPanel, Vector3.zero); // Đặt scale panel về 0 khi bắt đầu
//        SetPanelScale(upgradeTowerPanel, Vector3.zero); // Đặt scale panel về 0 khi bắt đầu

//        // Thêm sự kiện cho các nút đóng panel
//        if (closeBuyPanelButton != null)
//        {
//            closeBuyPanelButton.GetComponent<Button>().onClick.AddListener(CloseBuyPanel);
//        }
//        if (closeUpgradePanelButton != null)
//        {
//            closeUpgradePanelButton.GetComponent<Button>().onClick.AddListener(CloseUpgradePanel);
//        }
//    }

//    void Update()
//    {
//        // Kiểm tra sự kiện nhấp chuột
//        if (Input.GetMouseButtonDown(0))
//        {
//            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Collider2D hit = Physics2D.OverlapPoint(mousePosition);

//            if (hit != null)
//            {
//                // Xác định ô tháp nào đã được nhấp
//                for (int i = 0; i < towerSlots.Length; i++)
//                {
//                    if (hit.gameObject == towerSlots[i])
//                    {
//                        OnSlotClicked(i);
//                        break;
//                    }
//                }
//            }
//        }
//    }

//    void OnSlotClicked(int index)
//    {
//        if (towerSlots[index].transform.childCount == 0)
//        {
//            // Nếu ô chưa có tháp, hiển thị panel mua tháp
//            buyTowerPanel.SetActive(true);
//            upgradeTowerPanel.SetActive(false);
//            // Đặt kích thước panel về 0 trước khi phóng to
//            SetPanelScale(buyTowerPanel, Vector3.zero);
//            // Phóng to panel mua tháp
//            buyTowerPanel.transform.DOScale(Vector3.one, 0.5f);

//            // Truyền chỉ số của ô tháp vào TowerDisplayManager
//            towerDisplayManager.SetSelectedSlotIndex(index);
//        }
//        else
//        {
//            // Nếu ô đã có tháp, hiển thị panel nâng cấp tháp
//            buyTowerPanel.SetActive(false);
//            upgradeTowerPanel.SetActive(true);
//            // Đặt kích thước panel về 0 trước khi phóng to
//            SetPanelScale(upgradeTowerPanel, Vector3.zero);
//            // Phóng to panel nâng cấp tháp
//            upgradeTowerPanel.transform.DOScale(Vector3.one, 0.5f);
//        }
//    }

//    private void CloseBuyPanel()
//    {
//        if (buyTowerPanel.activeSelf)
//        {
//            // Thu nhỏ panel mua tháp trước khi ẩn
//            buyTowerPanel.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
//            {
//                buyTowerPanel.SetActive(false);
//            });
//        }
//    }

//    private void CloseUpgradePanel()
//    {
//        if (upgradeTowerPanel.activeSelf)
//        {
//            // Thu nhỏ panel nâng cấp tháp trước khi ẩn
//            upgradeTowerPanel.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() =>
//            {
//                upgradeTowerPanel.SetActive(false);
//            });
//        }
//    }

//    private void SetPanelScale(GameObject panel, Vector3 scale)
//    {
//        if (panel != null)
//        {
//            panel.transform.localScale = scale;
//        }
//    }
//    public void RemoveTower(Tower tower)
//    {
//        foreach (GameObject slot in towerSlots)
//        {
//            if (slot.transform.childCount > 0 && slot.transform.GetChild(0).gameObject == tower.gameObject)
//            {
//                Destroy(tower.gameObject); // Xóa tháp khỏi game
//                break;
//            }
//        }
//    }

//}

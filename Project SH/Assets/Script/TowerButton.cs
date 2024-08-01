using UnityEngine;
using UnityEngine.UI;

public class TowerButtonHandler : MonoBehaviour
{
    public int towerIndex; // Chỉ số tháp
    public TowerManagerUI towerManagerUI; // UI quản lý thông tin tháp

    // Sự kiện khi bấm vào nút
    public void OnButtonClick()
    {
        // Gọi hàm hiển thị thông tin tháp trong UI quản lý
        towerManagerUI.OnTowerPanelClicked(towerIndex);
    }
}

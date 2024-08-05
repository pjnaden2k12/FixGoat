using UnityEngine;
using UnityEngine.UI;

public class TowerButtonHandler : MonoBehaviour
{
    public int towerIndex; // Chỉ số của tháp

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        // Hiển thị thông tin của tháp được bấm
        TowerManagerUI.Instance.OnTowerPanelClicked(towerIndex);
    }
}

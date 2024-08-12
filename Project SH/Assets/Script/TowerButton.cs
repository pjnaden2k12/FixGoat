using UnityEngine;
using UnityEngine.UI;

public class EmptySlot : MonoBehaviour
{
    public int slotIndex; // Chỉ số của vị trí trống
    public Button slotButton;

    private void Start()
    {
        // Gán sự kiện cho nút khi bấm vào vị trí trống
        slotButton.onClick.AddListener(OnSlotClicked);
    }

    private void OnSlotClicked()
    {
        // Mở panel mua tháp khi nhấn vào vị trí trống
        TowerManagerInGame.Instance.OpenBuyPanel(slotIndex);
    }
}

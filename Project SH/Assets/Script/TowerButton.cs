using UnityEngine;

public class TowerPlacementManager : MonoBehaviour
{
    public static TowerPlacementManager Instance;

    public Transform[] placementPositions; // Các vị trí có thể đặt tháp

    private void Awake()
    {
        // Đảm bảo chỉ có một TowerPlacementManager trong cảnh
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ TowerPlacementManager qua các cảnh
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Phương thức để lấy vị trí trống
    public Transform GetEmptyPosition()
    {
        foreach (var position in placementPositions)
        {
            if (position.childCount == 0) // Kiểm tra nếu vị trí chưa có tháp
            {
                return position;
            }
        }
        return null;
    }

    // Phương thức để đặt tháp vào vị trí
    public void PlaceTower(GameObject towerPrefab, Transform position)
    {
        if (position.childCount == 0) // Kiểm tra nếu vị trí chưa có tháp
        {
            Instantiate(towerPrefab, position.position, Quaternion.identity, position);
        }
    }
}

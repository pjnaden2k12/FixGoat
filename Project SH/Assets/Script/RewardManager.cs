using UnityEngine;
using TMPro; // Đảm bảo bạn đã thêm TextMesh Pro vào dự án của mình

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance { get; private set; } // Singleton pattern
    public GameObject rewardPanel; // Gán prefab panel phần thưởng trong Unity Editor
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI diamondsText;
    public TextMeshProUGUI towerPiecesText;

    private void Start()
    {
        rewardPanel.SetActive(false);
    }
    private void Awake()
    {
        // Singleton pattern: đảm bảo chỉ có một instance duy nhất của RewardManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowRewardPanel(int levelId)
    {
        var rewards = GenerateBossRewards(levelId); // Tính phần thưởng dựa trên levelId
        rewardPanel.SetActive(true);
        goldText.text = "Gold: " + rewards.gold;
        diamondsText.text = "Diamonds: " + rewards.diamonds;
        towerPiecesText.text = "Tower Pieces: " + rewards.towerPieces;
    }

    private (int gold, int diamonds, int towerPieces) GenerateBossRewards(int levelId)
    {
        // Tính phần thưởng dựa trên levelId
        int goldReward = Random.Range(500 + levelId * 50, 2000 + levelId * 100);
        int diamondReward = Random.Range(10 + levelId * 5, 90 + levelId * 10);
        int towerPieceReward = Random.Range(10 + levelId * 5, 90 + levelId * 10);

        // Cập nhật số lượng phần thưởng trong ResourceManager nếu cần
        ResourceManager.Instance.AddGold(goldReward);
        ResourceManager.Instance.AddDiamonds(diamondReward);
        ResourceManager.Instance.AddTowerPieces(towerPieceReward);

        return (goldReward, diamondReward, towerPieceReward);
    }
}

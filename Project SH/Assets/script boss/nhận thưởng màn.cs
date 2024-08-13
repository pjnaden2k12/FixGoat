//using UnityEngine;
//using TMPro; // Đảm bảo bạn đã thêm TextMesh Pro vào dự án của mình

//public class RewardManager : MonoBehaviour
//{
//    public GameObject rewardPanel; // Gán prefab panel phần thưởng trong Unity Editor
//    public TextMeshProUGUI goldText;
//    public TextMeshProUGUI diamondsText;
//    public TextMeshProUGUI towerPiecesText;

//    private void OnEnable()
//    {
//        PrefabDestroyNotifier.OnPrefabDestroyed += HandlePrefabDestroyed;
//    }

//    private void OnDisable()
//    {
//        PrefabDestroyNotifier.OnPrefabDestroyed -= HandlePrefabDestroyed;
//    }

//    private void HandlePrefabDestroyed(GameObject prefab)
//    {
//        Boss boss = prefab.GetComponent<Boss>(); // Lấy thông tin boss từ prefab

//        if (boss != null)
//        {
//            int levelId = boss.levelId; // Lấy levelId từ boss
//            var rewards = GenerateBossRewards(levelId);

//            Hiển thị phần thưởng
//            ShowRewardPanel(rewards);
//        }
//    }

//    public (int gold, int diamonds, int towerPieces) GenerateBossRewards(int levelId)
//    {
//        int goldReward = Random.Range(500 + levelId * 50, 2000 + levelId * 100);
//        int diamondReward = Random.Range(10 + levelId * 5, 90 + levelId * 10);
//        int towerPieceReward = Random.Range(10 + levelId * 5, 90 + levelId * 10);

//        ResourceManager.Instance.AddGold(goldReward);
//        ResourceManager.Instance.AddDiamonds(diamondReward);
//        ResourceManager.Instance.AddTowerPieces(towerPieceReward);

//        return (goldReward, diamondReward, towerPieceReward);
//    }

//    private void ShowRewardPanel((int gold, int diamonds, int towerPieces) rewards)
//    {
//        rewardPanel.SetActive(true);
//        goldText.text = "Gold: " + rewards.gold;
//        diamondsText.text = "Diamonds: " + rewards.diamonds;
//        towerPiecesText.text = "Tower Pieces: " + rewards.towerPieces;
//    }
//}

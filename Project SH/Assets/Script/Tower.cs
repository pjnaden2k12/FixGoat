using UnityEngine;
using static TowerManager;

public class Tower : MonoBehaviour
{
    public int id;
    public int level = 1;
    public int upgradeCost = 50;
    public int maxLevel = 3;

    private float damage;
    private float attackSpeed;
    private float range;

    void Start()
    {
        // Đăng ký tháp với TowerManager
        if (TowerManager.Instance != null)
        {
            TowerManager.Instance.RegisterTower(this);
        }
        UpdateStats();
    }

    private void OnDestroy()
    {
        // Hủy đăng ký tháp với TowerManager
        if (TowerManager.Instance != null)
        {
            TowerManager.Instance.UnregisterTower(this);
        }
    }

    public void Upgrade()
    {
        if (GearManager.Instance != null && GearManager.Instance.SpendGears(upgradeCost))
        {
            if (level < maxLevel)
            {
                level++;
                UpdateStats();
            }
            else
            {
                Debug.LogWarning("Tower is already at maximum level!");
            }
        }
        else
        {
            Debug.LogWarning("Not enough gears to upgrade the tower!");
        }
    }

    public void UpdateStats()
    {
        TowerData towerData = TowerManager.Instance.GetTowerDataById(id);
        if (towerData != null)
        {
            switch (level)
            {
                case 1:
                    damage = towerData.baseDamage * 1f;
                    attackSpeed = towerData.baseAttackSpeed * 1f;
                    range = towerData.baseRange * 1f;
                    break;
                case 2:
                    damage = towerData.baseDamage * 2f;
                    attackSpeed = towerData.baseAttackSpeed * 1.5f;
                    range = towerData.baseRange * 1.2f;
                    break;
                case 3:
                    damage = towerData.baseDamage * 3f;
                    attackSpeed = towerData.baseAttackSpeed * 2f;
                    range = towerData.baseRange * 1.5f;
                    break;
            }
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetRange()
    {
        return range;
    }
}

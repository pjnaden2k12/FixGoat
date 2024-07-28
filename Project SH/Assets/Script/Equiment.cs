using UnityEngine;

public class Equipment
{
    public float healthBoost;
    public float defenseBoost;
    public float healingBoost;
    public int upgradeLevel;
    public int maxUpgradeLevel = 10;
    public int upgradeCost;

    public Equipment(float healthBoost, float defenseBoost, float healingBoost, int upgradeCost)
    {
        this.healthBoost = healthBoost;
        this.defenseBoost = defenseBoost;
        this.healingBoost = healingBoost;
        this.upgradeLevel = 0;
        this.upgradeCost = upgradeCost;
    }

    public bool Upgrade(ref int gold)
    {
        if (upgradeLevel < maxUpgradeLevel && gold >= upgradeCost)
        {
            upgradeLevel++;
            gold -= upgradeCost;
            upgradeCost *= 2; // T?ng chi phí nâng c?p theo c?p ??
            // T?ng các ch? s? theo c?p ?? (?i?u ch?nh các h? s? này theo game c?a b?n)
            healthBoost *= 1.1f;
            defenseBoost *= 1.1f;
            healingBoost *= 1.1f;
            return true;
        }
        return false;
    }
}

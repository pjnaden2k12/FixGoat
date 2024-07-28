using UnityEngine;

public class Character : MonoBehaviour
{
    public int gold = 1000; // Số vàng ban đầu của người chơi
    public Equipment helmet;
    public Equipment armor;
    public Equipment pants;
    public Equipment boots;

    public (float, float, float) CalculateWallBuffs()
    {
        float totalHealthBoost = (helmet?.healthBoost ?? 0) + (armor?.healthBoost ?? 0) + (pants?.healthBoost ?? 0) + (boots?.healthBoost ?? 0);
        float totalDefenseBoost = (helmet?.defenseBoost ?? 0) + (armor?.defenseBoost ?? 0) + (pants?.defenseBoost ?? 0) + (boots?.defenseBoost ?? 0);
        float totalHealingBoost = (helmet?.healingBoost ?? 0) + (armor?.healingBoost ?? 0) + (pants?.healingBoost ?? 0) + (boots?.healingBoost ?? 0);

        return (totalHealthBoost, totalDefenseBoost, totalHealingBoost);
    }

    public bool UpgradeEquipment(string slot)
    {
        Equipment equipment = null;

        switch (slot)
        {
            case "helmet":
                equipment = helmet;
                break;
            case "armor":
                equipment = armor;
                break;
            case "pants":
                equipment = pants;
                break;
            case "boots":
                equipment = boots;
                break;
        }

        if (equipment != null)
        {
            return equipment.Upgrade(ref gold);
        }
        return false;
    }
}

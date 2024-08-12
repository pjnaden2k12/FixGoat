using System.IO;
using UnityEngine;

public class ResourceDataSaver
{
    private string resourcesFilePath;

    public ResourceDataSaver()
    {
        string folderName = "resourceSave";
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        resourcesFilePath = Path.Combine(folderPath, "resources.txt");
    }

    public ResourceData LoadResources()
    {
        var data = new ResourceData();
        if (File.Exists(resourcesFilePath))
        {
            try
            {
                string[] lines = File.ReadAllLines(resourcesFilePath);
                if (lines.Length >= 10)
                {
                    data.gold = int.Parse(lines[0].Split(':')[1].Trim());
                    data.diamonds = int.Parse(lines[1].Split(':')[1].Trim());
                    data.towerPieces = int.Parse(lines[2].Split(':')[1].Trim());
                    data.universalStones = int.Parse(lines[3].Split(':')[1].Trim());
                    data.wallHealthBonus = int.Parse(lines[4].Split(':')[1].Trim());
                    data.wallDefenseBonus = int.Parse(lines[5].Split(':')[1].Trim());
                    data.healthRegenBonus = int.Parse(lines[6].Split(':')[1].Trim());
                    data.towerDamageBonus = float.Parse(lines[7].Split(':')[1].Trim());
                    data.towerAttackSpeedBonus = float.Parse(lines[8].Split(':')[1].Trim());
                    data.evolutionLevel = int.Parse(lines[9].Split(':')[1].Trim());
                }
                else
                {
                    Debug.LogWarning("Dữ liệu trong tệp không đầy đủ.");
                    SetDefaultValues(data);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Lỗi khi nạp tài nguyên: " + ex.Message);
                SetDefaultValues(data);
            }
        }
        else
        {
            SetDefaultValues(data);
        }
        return data;
    }

    public void SaveResources(ResourceData data)
    {
        string content = $"Gold: {data.gold}\n" +
                         $"Diamonds: {data.diamonds}\n" +
                         $"Tower Pieces: {data.towerPieces}\n" +
                         $"Universal Stones: {data.universalStones}\n" +
                         $"Wall Health Bonus: {data.wallHealthBonus}\n" +
                         $"Wall Defense Bonus: {data.wallDefenseBonus}\n" +
                         $"Health Regen Bonus: {data.healthRegenBonus}\n" +
                         $"Tower Damage Bonus: {data.towerDamageBonus}\n" +
                         $"Tower Attack Speed Bonus: {data.towerAttackSpeedBonus}\n" +
                         $"Evolution Level: {data.evolutionLevel}";

        File.WriteAllText(resourcesFilePath, content);
    }

    private void SetDefaultValues(ResourceData data)
    {
        data.gold = 99999;
        data.diamonds = 1999;
        data.towerPieces = 1000;
        data.universalStones = 0;
        data.wallHealthBonus = 0;
        data.wallDefenseBonus = 0;
        data.healthRegenBonus = 0;
        data.towerDamageBonus = 0;
        data.towerAttackSpeedBonus = 0;
        data.evolutionLevel = 0;
    }
}

[System.Serializable]
public class ResourceData
{
    public int gold;
    public int diamonds;
    public int towerPieces;
    public int universalStones;
    public int wallHealthBonus;
    public int wallDefenseBonus;
    public int healthRegenBonus;
    public float towerDamageBonus;
    public float towerAttackSpeedBonus;
    public int evolutionLevel;
}

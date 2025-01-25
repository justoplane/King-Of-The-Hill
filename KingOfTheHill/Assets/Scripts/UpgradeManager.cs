using UnityEditor.Rendering.Universal;
using UnityEngine;
public struct Upgrade
{
    public string name;
    public int damage;
    public int attackSpeed;
    public int range;
    public int cost;
    public bool active;
    public Utils.ParentObject parentObject;
    public int pathNumber;
    public int upgradeNumber;
    public bool unlocked;
}
public static class UpgradeManager
{

    public static Upgrade GetUpgradeInfo(Utils.ParentObject type, int pathNumber, int upgradeNumber)
    {
        // 
        switch (type)
        {
            case Utils.ParentObject.Mage:
                return GetMageUpgrade(pathNumber, upgradeNumber);
            case Utils.ParentObject.Knight:
                return GetKnightUpgrade(pathNumber, upgradeNumber);
            case Utils.ParentObject.MageTower:
                return GetMageTowerUpgrade(pathNumber, upgradeNumber);
            case Utils.ParentObject.KnightTower:
                return GetKnightTowerUpgrade(pathNumber, upgradeNumber);
            default:
                return GetKnightTowerUpgrade(pathNumber, upgradeNumber);
        }
    }

    static Upgrade GetKnightUpgrade(int pathNumber, int upgradeNumber)
    {
        foreach (Upgrade upgrade in KnightUpgrades.upgradeList)
        {
            if (upgrade.pathNumber == pathNumber && upgrade.upgradeNumber == upgradeNumber)
            {
                return upgrade;
            }
        }
        return KnightUpgrades.upgradeList[0];
    }
    static Upgrade GetMageUpgrade(int pathNumber, int upgradeNumber)
    {
        foreach (Upgrade upgrade in MageUpgrades.upgradeList)
        {
            if (upgrade.pathNumber == pathNumber && upgrade.upgradeNumber == upgradeNumber)
            {
                return upgrade;
            }
        }
        return MageUpgrades.upgradeList[0];
    }

    static Upgrade GetKnightTowerUpgrade(int pathNumber, int upgradeNumber)
    {
        foreach (Upgrade upgrade in KnightTowerUpgrades.upgradeList)
        {
            if (upgrade.pathNumber == pathNumber && upgrade.upgradeNumber == upgradeNumber)
            {
                return upgrade;
            }
        }
        return KnightTowerUpgrades.upgradeList[0];
    }

    static Upgrade GetMageTowerUpgrade(int pathNumber, int upgradeNumber)
    {
        foreach (Upgrade upgrade in MageTowerUpgrades.upgradeList)
        {
            if (upgrade.pathNumber == pathNumber && upgrade.upgradeNumber == upgradeNumber)
            {
                return upgrade;
            }
        }
        return MageTowerUpgrades.upgradeList[0];
    }
}

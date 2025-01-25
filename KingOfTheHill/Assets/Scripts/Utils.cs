using UnityEngine;

public class Utils
{
    public enum DamageType
    {
        Physical,
        Magical,
    }

    public enum ParentObject
    {
        Mage,
        Knight,
        MageTower,
        KnightTower,
    }

    public enum Phase
    {
        Prep,
        Combat,
        Reward,
        Upgrade,
    }

    public enum TargetPriority
    {
        First,
        Strong,
        Last,
        Close,
    }

}

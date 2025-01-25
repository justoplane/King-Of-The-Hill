using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnightUpgrades", menuName = "Scriptable Objects/KnightUpgrades")]
public static class KnightUpgrades
{
    public static List<Upgrade> upgradeList = new List<Upgrade>
    {
        new Upgrade { name = "Upgrade1", damage = 1, active = true, cost = 100, pathNumber = 1, upgradeNumber = 1, unlocked = false },
        new Upgrade { name = "Upgrade2", damage = 2, active = true, cost = 200, pathNumber = 1, upgradeNumber = 2, unlocked = false },
        new Upgrade { name = "Upgrade3", damage = 3, active = true, cost = 300, pathNumber = 1, upgradeNumber = 3, unlocked = false },
        new Upgrade { name = "Upgrade4", damage = 4, active = true, cost = 400, pathNumber = 1, upgradeNumber = 4, unlocked = false },
 
        new Upgrade { name = "Upgrade1", damage = 1, active = true, cost = 100, pathNumber = 1, upgradeNumber = 1, unlocked = false },
        new Upgrade { name = "Upgrade2", damage = 2, active = true, cost = 200, pathNumber = 1, upgradeNumber = 2, unlocked = false },
        new Upgrade { name = "Upgrade3", damage = 3, active = true, cost = 300, pathNumber = 1, upgradeNumber = 3, unlocked = false },
        new Upgrade { name = "Upgrade4", damage = 4, active = true, cost = 400, pathNumber = 1, upgradeNumber = 4, unlocked = false },
    };
}

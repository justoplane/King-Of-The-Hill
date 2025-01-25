using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    int economy;
    UpgradeManager upgradeManager;
    List<Unit> units;
    List<Tower> towers;
    Utils.Role role;

    public List<Unit> getUnits()
    {
        return units;
    }
    public List<Tower> getTowers()
    {
        return towers;
    }

    public Utils.Role getRole()
    {
        return role;
    }
}

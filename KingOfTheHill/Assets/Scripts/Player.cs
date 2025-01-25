using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    int economy;
    List<GameObject> units;
    List<GameObject> towers;
    Utils.Role role;

    public Player(Utils.Role role)
    {
        this.role = role;
        economy = 0;
        units = new List<GameObject>();
        towers = new List<GameObject>();
    }

    public List<GameObject> getUnits()
    {
        return units;
    }
    public List<GameObject> getTowers()
    {
        return towers;
    }

    public Utils.Role getRole()
    {
        return role;
    }

    public void addUnit(GameObject unit)
    {
        units.Add(unit);
    }
}

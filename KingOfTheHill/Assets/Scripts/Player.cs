using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    int economy;
    List<Unit> units;
    List<Tower> towers;
    Utils.Role role;

    public Player(Utils.Role role)
    {
        this.role = role;
        economy = 0;
        units = new List<Unit>();
        towers = new List<Tower>();
    }

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

    public void addUnit(Utils.ParentObject type, Path path)
    {
        // Add a new unit to the player's list of units
        switch (type)
        {
            case Utils.ParentObject.Knight:
                // GameObject newKnight = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                // newKnight.GetComponent<Knight>().printSomething();
                //units.Add(new Knight());
                break;
            case Utils.ParentObject.Mage:
                units.Add(new Mage());
                break;
            default:
                break;
        }
    }
}

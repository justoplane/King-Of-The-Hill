using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    protected string name;
    protected int damage;
    protected int attackSpeed;
    protected int range;
    protected bool active;
    protected Utils.DamageType damageType;
    protected List<Upgrade> upgrades;
}

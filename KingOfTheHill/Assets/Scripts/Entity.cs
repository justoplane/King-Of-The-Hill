using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    protected string name;
    protected int health;
    protected int damage;
    protected float attackSpeed;
    protected double timeSinceLastAttack;
    protected int range;
    protected bool active;
    protected Utils.DamageType damageType;
    protected List<Upgrade> upgrades;
    protected Entity target;

    public bool canAttack()
    {
        return true; // TODO: Implement attack cooldown 
    }
    public void doAnimation()
    {
        // Play attack animation
    }

    public bool isActive()
    {
        return active;
    }

    public Entity getTarget()
    {
        return target;
    }

    public int getDamage()
    {
        // TODO: Calculate damage based on upgrades
        return damage;
    }

    public void takeDamage(int damageTaken)
    {
        health -= damageTaken;
    }

    public int getHealth()
    {
        return health;
    }
}

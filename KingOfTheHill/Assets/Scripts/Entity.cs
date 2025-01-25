using UnityEngine;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{
    protected int health;
    protected int damage;

    protected float attackSpeed;
    protected float timeSinceLastAttack;
    protected int range;
    protected bool active;
    protected Utils.DamageType damageType;
    protected List<Upgrade> upgrades;
    protected Entity target;
    protected float attackCooldown;
    protected float lastAttackTime;

    public bool CanAttack() {
        if (!IsActive()) {
            return false;
        }

        if (Time.time < lastAttackTime + attackCooldown) {
            return false;
        }

        lastAttackTime = Time.time;
        return true;
    }
    public void DoAnimation() {
        // Play attack animation
    }

    public bool IsActive() {
        return active;
    }

    public Entity GetTarget() {
        return target;
    }

    public int GetDamage() {
        // TODO: Calculate damage based on upgrades
        return damage;
    }

    public void TakeDamage(int damageTaken) {
        health -= damageTaken;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetHealth() {
        return health;
    }

    public void Die() {
        Destroy(gameObject);
    }
}

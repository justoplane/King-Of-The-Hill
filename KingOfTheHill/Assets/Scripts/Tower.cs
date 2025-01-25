using UnityEngine;

public class Tower : Entity
{
    protected Utils.TargetPriority targetPriority;

    private void Start() {
        
    }

    public void Attack(Unit target) {
        // todo: play attack animation
        target.TakeDamage(damage);
    }

    public void Update() {
        if (CanAttack()) {
            // TODO: find nearest target
            // Attack(target);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;

public class MageTower : Tower
{
    public List<Unit> unitsInRange;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unitsInRange = new List<Unit>();
        damage = 40;
        lastAttackTime = 0;
        attackCooldown = 2;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true && unitsInRange.Count > 0 && CanAttack()){
            if(target != null){
                animator.SetBool("isAttacking", true);
                timeSinceLastAttack = 0;
                target.TakeDamage(GetDamage());
            }
        } else {
            animator.SetBool("isAttacking", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Unit"){
            unitsInRange.Add(other.gameObject.GetComponent<Unit>());
        }
        if(unitsInRange.Count > 0){
            updateTarget(unitsInRange[0]);
            active = true;
        } else {
            active = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXITED TOWER COLLIDER");
        if(other.gameObject.tag == "Unit" && unitsInRange.Contains(other.gameObject.GetComponent<Unit>())){
            unitsInRange.Remove(other.gameObject.GetComponent<Unit>());
        }
        if(unitsInRange.Count > 0){
            updateTarget(unitsInRange[0]);
            active = true;
        } else {
            active = false;
        }
    }

    void updateTarget(Entity newTarget){
        target = newTarget;
    }
}

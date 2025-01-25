using UnityEngine;
using System.Collections.Generic;

public class MageTower : Tower
{

    public List<Unit> unitsInRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unitsInRange = new List<Unit>();
        damage = 1;
        attackSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        Debug.Log(canAttack());
        if(active == true && unitsInRange.Count > 0 && canAttack()){
            if(target != null){
                timeSinceLastAttack = 0;
                target.takeDamage(getDamage());
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTERD TOWER COLLIDER");
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

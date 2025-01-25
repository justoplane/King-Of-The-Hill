using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Knight : Unit
{
    // These are values that are specific to the KNIGHT CLASS

    private void Awake()
    {
        name = "Knight";
        health = 100;
        damage = 10;
        attackSpeed = 1;
        timeSinceLastAttack = 0;
        range = 1;
        active = true;
        damageType = Utils.DamageType.Physical;
        upgrades = new List<Upgrade>();
        target = null;

        this.anim = gameObject.GetComponent<Animator>();

    }

    private Animator anim;

// Start is called once before the first execution of Update after the MonoBehaviour is created

// Update is called once per frame
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space)){
        if (this.anim != null)
        {
            // play Bounce but start at a quarter of the way through
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", true);
        }
    }
    if (Input.GetKeyDown(KeyCode.D)){
        if (this.anim != null)
        {
            // play Bounce but start at a quarter of the way through
            anim.SetBool("isWalking", true);
            anim.SetBool("isAttacking", false);
        }
    }
}
}

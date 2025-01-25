using UnityEngine;
using System.Collections;

public class Knight : Unit
{   
    private Animator anim;
    public Knight(){
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){

            if (this.anim != null)
            {
                // play Bounce but start at a quarter of the way through
                anim.SetBool("isAttacking", true);
            }
        }
    }
}

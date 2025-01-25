using UnityEngine;
using System.Collections;

public class Mage : Unit
{
    private Animator anim;
    public GameObject firevballSpawnPosObj;
    // public Mage(Path path) : base(path) {
        
    // }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.anim = gameObject.GetComponent<Animator>();
        this.firevballSpawnPosObj = transform.Find("Fireball_Spawn_Loc").gameObject;
    }

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

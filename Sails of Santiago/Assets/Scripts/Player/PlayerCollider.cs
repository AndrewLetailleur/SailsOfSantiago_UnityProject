using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour {

    /// <summary> This is the Player Collision script that auto-sorts collider calls
    /// 1st, it checks if it's a sail or hull collider, and sets a flag
    /// 2nd, if the collider it's attached to is hit by cannon fire,
    ///     it calls upon the damage function on a hull or sail for the player
    /// </summary> Was split into Sail and Hull collider's, but I simplified the code to a universal one, ish. ;)

    public GameObject hitbox;/*set as linked to parent, hack wiser*/
    private PlayerShipControls_Script playRef; /*the script itself, player only wise*/
    public float damage = 10f; //test variable
    public bool Saily, Hully = false;/*test triggers*/

    // Use this for initialization
    void Start() {
        hitbox = GameObject.FindGameObjectWithTag("Player");
        playRef = hitbox.GetComponent<PlayerShipControls_Script>();//grab the script

        if (this.gameObject.tag == "Hull")
            Hully = true;
        else if (this.gameObject.tag == "Sail")
            Saily = true;
        //endif

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnemyShot" && Saily)
        {
            Debug.Log("Sail is damaged!!!"); 
            playRef.SailDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }

        if (other.gameObject.tag == "EnemyShot" && Hully)
        {
            Debug.Log("Hull is damaged!!!");
            playRef.HullDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }
    }
}

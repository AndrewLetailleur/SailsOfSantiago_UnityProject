using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Code Written by; Andrew Letailleur (2018) */

public class EnemyCollider : MonoBehaviour {

    /// <summary> This is the Enemy Collision script that auto-sorts collider calls
    /// Similar to PlayerCollider in getting hp_type flags, and checking for colliders
    /// With the only difference being, outside variable health/damage... Script references. 
    /// </summary> ///PS: Sollution found in enemy script has been parraelled here, since it's less assets managed/est

        ///not needed, as GetComponentInParent works FANTASTIC!
    //public GameObject hitbox;/*set as linked to parent, hack wiser*/
    public BaseEnemyScript baseRef;
    public float damage = 10f; //test variable
    public bool Saily, Hully = false;/*test triggers, not used atm*/

    // Use this for initialization
    void Start () {
        ///old, buggy and not reliable code. [REDACTED]
        //get parent test hitbox = this.gameObject.GetComponentInParent;
        /// <summary> begin convoluted IF tree Mk2//after the long ass tree, hacker wise
        /// ===
        /// started writing convoluted code string including a bunch of if's,
        /// else's and a harem of a renagade time lords...
        /// Before this problem took an arrow to the knee 
        /// </summary> ///end code hassle
        //end  //baseRef = hitbox.GetComponent<BaseEnemyScript>();//grab the script

        /// <summary> based on the 'GetComponentsInParent' thingie, without an 's' in it
        /// https://docs.unity3d.com/ScriptReference/GameObject.GetComponentsInParent.html
        /// </summary> 
            //new better code
        baseRef = GetComponentInParent<BaseEnemyScript>();//only one, as I JUST need the script, and no more.
        
        //this part is grabbed from player asset
        if (this.gameObject.tag == "Hull")
            Hully = true;
        else if (this.gameObject.tag == "Sail")
            Saily = true;
        //endif
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerShot" && Hully) /*test prefab for now*/
        {
            Debug.Log("Hull is damaged FOE!!!");
            baseRef.TestDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc. AFTOMH, doesn't even work.
        }

        if (other.gameObject.tag == "PlayerShot" && Saily) /*test prefab for now*/
        {
            Debug.Log("Sails are damaged FOE!!!");
            baseRef.TestDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc. AFTOMH, doesn't even work.
        }
    }
 }

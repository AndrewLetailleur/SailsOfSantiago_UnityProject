using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullCollider : MonoBehaviour {

    /// <summary> This is the Hull Collision script
    /// It's duty; to check if hit by cannon fire/est, before calling a player function
    /// </summary> Based on the Sail Collision script

    public GameObject hitbox;/*set as linked to parent, hack wiser*/
    private PlayerShipControls_Script playRef;
    private PlayerShipControls_Script enemyRef; /*enemy for now, hack wise*/
    public float damage = 10f; //test variable

    // Use this for initialization
    void Start() { }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnemyShot" && hitbox.gameObject.tag == "Player")
        {
            Debug.Log("Hull is damaged!!!");
            playRef = hitbox.GetComponent<PlayerShipControls_Script>();//grab the script
            playRef.HullDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }

        if (other.gameObject.tag == "PlayerShot" && playRef.gameObject.tag == "Enemy")
        {
            enemyRef = hitbox.GetComponent<PlayerShipControls_Script>();//grab the script
            Debug.Log("Enemy Destroyed");
            enemyRef.HullDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }
    }
}

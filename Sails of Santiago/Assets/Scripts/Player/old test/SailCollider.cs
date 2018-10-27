using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailCollider : MonoBehaviour {

    /// <summary> This is the Hull Collision script
    /// It's duty; to check if hit by cannon fire/est, before calling a player function
    /// </summary> The first script which worked, via workarounds on inheritence

    public GameObject hitbox;/*set as linked to parent, hack wiser*/
    private PlayerShipControls_Script playRef;
    private PlayerShipControls_Script enemyRef; /*enemy for now, hack wise. Lazy script reference wise*/
    public float damage = 10f; //test variable

    private bool Saily, Hully;

    // Use this for initialization
    void Start () {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "EnemyShot" && hitbox.gameObject.tag == "Player")
        {
            Debug.Log("Hull is damaged!!!");
            playRef = hitbox.GetComponent<PlayerShipControls_Script>();//grab the script
            playRef.SailDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }

        if (other.gameObject.tag == "PlayerShot" && playRef.gameObject.tag == "Enemy")
        {
            enemyRef = hitbox.GetComponent<PlayerShipControls_Script>();//grab the script
            Debug.Log("Enemy Destroyed");
            enemyRef.SailDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }
    }

}

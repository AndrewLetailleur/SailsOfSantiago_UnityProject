using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullCollider : MonoBehaviour {

    /// <summary> This is the Hull Collision script
    /// It's duty; to check if hit by cannon fire/est, before calling a player function
    /// </summary> Based on the Sail Collision script

    private GameObject player;
    private PlayerShipControls_Script playRef;
    public float damage = 10f; //test variable

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playRef = player.GetComponent<PlayerShipControls_Script>();//grab the script
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hull is damaged!!!");
        if (other.gameObject.tag == "EnemyShot")
        {
            playRef.HullDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }
    }
}

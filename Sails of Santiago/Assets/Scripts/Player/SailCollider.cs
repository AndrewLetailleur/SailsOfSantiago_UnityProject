﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailCollider : MonoBehaviour {

    /// <summary> This is the Hull Collision script
    /// It's duty; to check if hit by cannon fire/est, before calling a player function
    /// </summary> The first script which worked, via workarounds on inheritence

    private GameObject player;
    private PlayerShipControls_Script playRef;
    public float damage = 10f; //test variable

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playRef = player.GetComponent<PlayerShipControls_Script>();//grab the script

	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("It hitted the sail!");
        if (other.gameObject.tag == "EnemyShot") {
            playRef.SailDamage(damage);
            Destroy(other.gameObject);//destroy the collider afterwards, jnc
        }
    }

}

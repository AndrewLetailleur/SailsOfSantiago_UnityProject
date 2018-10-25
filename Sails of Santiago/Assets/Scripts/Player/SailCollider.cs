using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailCollider : MonoBehaviour {

    private GameObject player;
    private PlayerShipControls_Script playRef;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playRef = GetComponent<PlayerShipControls_Script>();//grab the script

	}

    private void OnTriggerEnter(Collider other)//if crashing say
    {
        if (other.gameObject.tag == "EnemyShot")
        {
            Debug.Log("It hitted the sail!");
            playRef.SailDamage(10f);//damage by 10f say
        }
    }
}

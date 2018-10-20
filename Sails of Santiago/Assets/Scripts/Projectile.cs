using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject splashFX, crashFX;//for the particles
    public float lifeSpan = 10f; //ten seconds approx.
    public bool waterFlag, hitFlag = false;//in advance
    private Rigidbody RB;
	// Use this for initialization
	void Start () {
        //destroy the game object 'this' belongs to
        Destroy(this.gameObject, lifeSpan); //
        RB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (waterFlag)
            Splash();//flag
        else if (hitFlag)
            Smash();//flag
        //end if flags of death conditions
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
            waterFlag = true;
        //else
            //hitFlag = true;
        //endif
    }

        //insert object destruction scripts
    void Splash() {
        //Instantiate(splashFX, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    void Smash() {
        //Instantiate(crashFX, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }//end smash code


}

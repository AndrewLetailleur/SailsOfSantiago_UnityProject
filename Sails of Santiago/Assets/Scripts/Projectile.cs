using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public GameObject splashFX, crashFX;//for the particles
    public float lifeSpan = 10f; //ten seconds approx.
    public float seaMulti = 5f;
    public bool waterFlag, hitFlag = false;//in advance
    private Rigidbody RB;
	// Use this for initialization
	void Start () {
        //destroy the game object 'this' belongs to
        //Destroy(this.gameObject, lifeSpan); //
        RB = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (waterFlag)
            lifeSpan -= Time.deltaTime * seaMulti;
        else
            lifeSpan -= Time.deltaTime * 1;
        if (hitFlag)
            Smash();//flag
        //end if flags of death conditions

        if (lifeSpan <= 0)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            waterFlag = true;
            Splash();//flag
        }

        if (other.gameObject.tag == "Enemy" && this.gameObject.tag != "EnemyShot")
        {
            Smash();//flag
            Destroy(other.gameObject);//hack smash effect
        }

        else { 
            Smash();
        }
        //endif
    }

        //insert object destruction scripts
    void Splash() {
        Instantiate(splashFX, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }
    void Smash() {
        Instantiate(crashFX, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }//end smash code


}

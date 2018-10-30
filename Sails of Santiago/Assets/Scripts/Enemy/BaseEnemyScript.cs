using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//adds/uses NavMeshAgent, ref wise
/* Code Written by; Andrew Letailleur (2018) */

public class BaseEnemyScript : MonoBehaviour {

    //keep things public for now. Private when need be.

    public float HP_Max = 100F; //test prefab, unused assets are /*commented out*/
    public float HP_Test; /*HP_Sail, HP_Hull;*/
    //test being there for testy purposes

        //misc references to components
    public Transform target; //who the ship will chase after
    private Rigidbody RB;//the rigidbody
    public NavMeshAgent nav; //... May need to look on tutorials on recent versions, 

    //enemy ship attack variables
    public GameObject[] AttackGuns;/*LeftGuns, RightGuns;*/
    //

    public GameObject Projectile; //the enemy projectile


	// Use this for initialization
	void Start () {

        target = GameObject.FindGameObjectWithTag("Player").transform; //position wise
        nav = GetComponent<NavMeshAgent>();
        RB = GetComponent<Rigidbody>();

        //        HP_Hull = HP_Max;
        //        HP_Sail = HP_Max;
        HP_Test = HP_Max;
        HP_Update();
	}
	
	// Update is called once per frame
	void Update () {
        //vertical control & horizontal control
            //MoveShip();, RotateShip(); //by AI Troopers
        //buggy pirate flag code. Now with only toscale woes!
            //ChangeSails();
        //combat code
            //ShipAttack();//check firing pin's afterwards
        //the fire limiter
            //ReloadCode();//also does reloading, so there's no need TO reload :P
                  //testground for stuff
                  //TestVoider();
    }

    //begin movement code hack







    public void TestDamage(float damage)
    {//by amount
        Debug.Log("I'm hit! Enemy damaged!");
        HP_Test -= damage; //take away by amount
        HP_Update();
    }

    public void HP_Update()
    {//by amount
        if (HP_Test > HP_Max)
            HP_Test = HP_Max;
        else if (HP_Test <= 0) { 
            Destroy(this.gameObject);//destroy an enemy
            Debug.Log("I'm sinking!!!");
        }//endif
    }
    //end of prototype damage code


    //begin attack code



}

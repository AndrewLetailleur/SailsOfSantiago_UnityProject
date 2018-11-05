using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//adds/uses NavMeshAgent, ref wise
/* Code Written by; Andrew Letailleur (2018) */

public class BaseEnemyScript : MonoBehaviour {

    //keep things public for now. Private when need be.



    private float Max_FireRate = 5f;
    private float FireRate = 0f;
    private bool Loaded = true;

        //HP variables
    public float HP_Max = 100F; //test prefab, unused assets are /*commented out*/
    public float HP_Test; /*HP_Sail, HP_Hull;*/
    //test being there for testy purposes

        //misc references to components
    public Transform target; //who the ship will chase after
    private Rigidbody RB;//the rigidbody
        //
    public NavMeshAgent agent; //... May need to look on tutorials on recent versions, 
    public NavMeshPath path;
    public Vector3[] PlayerPaths;
        //movement variables
    public float distance;//distance
    private float min_dist = 45f;//for now


    //enemy ship attack variables
    public GameObject[] AttackGuns;/*LeftGuns, RightGuns;*/
    //leave AttackGuns for now, since it's coding hassle in getting the children
    public GameObject Projectile; //the enemy projectile, leave public for editing


	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform; //position wise
        agent = GetComponent<NavMeshAgent>();
        path = GetComponent<NavMeshPath>();//get THE path, per say?
        RB = GetComponent<Rigidbody>();
        

        //        HP_Hull = HP_Max;
        //        HP_Sail = HP_Max;
        HP_Test = HP_Max;
        HP_Update();
	}

    private void PlayerPathsFunction() {

        float var = 5f;

        for (int i = 0; i < PlayerPaths.Length; i++) {
            PlayerPaths[i] = target.position;
            PlayerPaths[i].y = this.transform.position.y; //to harmonise the y co-ordinate, hack wise.
        }
        PlayerPaths[0].x += var;
        PlayerPaths[1].x -= var;
        PlayerPaths[2].z += var;
        PlayerPaths[3].z -= var;
    }

	
	// Update is called once per frame
	private void Update () {

        PlayerPathsFunction();

        MoveShip();


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


    public GameObject ShipLeft, ShipRight;

    public bool trig = false;

    private void MoveShip() {
        //path pending?

        RaycastHit hit;
        Ray fireRayTest = new Ray(ShipLeft.transform.position, Vector3.right);//test

        if (Physics.Raycast(fireRayTest, out hit)) {
            if (hit.collider.tag == "Player")
                Debug.Log("Hitting player!");
            //endif
        }//end if, thank hit for being a collider tag

        if (target/*dist is set*/)
        {
            agent.enabled = true;
            agent.SetDestination(target.position);//send to target destination
        }
        else
            agent.enabled = false;
        //endif

        //blah blah if AT near enough, fire guns if aroud side?
            //TODO LATER, figure out how to get the enemy to 'circle' around the player, obstacle wise.
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;//adds/uses NavMeshAgent, ref wise
//using System; //for Array, hack wise. NLN
/* Code Written by; Andrew Letailleur (2018) */

public class BaseEnemyScript : MonoBehaviour {

    //keep things public for now. Private when need be.

    private int Kill_Value = 1; //since 1 = 1 kill say, score wise. Ideally, 10y later?
    private LazyScoreCount Score;

    private float Max_FireRate = 1.8f;
    private float FireRate = 0f;
    private bool Loaded = true;

        //HP variables
    public float HP_Max = 100F; //test prefab, unused assets are /*commented out*/
    private float HP_Test; /*HP_Sail, HP_Hull;*/
                           //test being there for testy purposes

        //misc references to components
    public Transform target; //who the ship will chase after
    private Rigidbody RB;//the rigidbody
        //
    public NavMeshAgent agent; //... May need to look on tutorials on recent versions, 
    public NavMeshPath path;
    //public Vector3[] PlayerPaths;
        //movement variables
    public float distance;//distance
    public float min_dist = 50f;//for now


    //enemy ship attack variables
    public GameObject[] AttackGuns;/*LeftGuns, RightGuns;*/
    //leave AttackGuns for now, since it's coding hassle in getting the children
    public GameObject Projectile; //the enemy projectile, leave public for editing


	// Use this for initialization
	void Start () {
        Score = GameObject.FindGameObjectWithTag("Score").GetComponent<LazyScoreCount>();
        target = GameObject.FindGameObjectWithTag("Player").transform; //position wise
        agent = GetComponent<NavMeshAgent>();
        //path = GetComponent<NavMeshPath>();//get THE path, per say?
        RB = GetComponent<Rigidbody>();
        

        //        HP_Hull = HP_Max;
        //        HP_Sail = HP_Max;
        HP_Test = HP_Max;
        HP_Update();
	} 
	
	// Update is called once per frame
	private void Update () {

        //PlayerPathsFunction();//[REDACTED] for now, as it's non-functioning
        distance = Vector3.Distance(target.position, transform.position);//get distance on a 2D plane

        if (!Loaded)
        {
            FireRate -= Time.deltaTime;
            if (FireRate <= 0)
                Loaded = true;
            //endif
        }
        else if (distance <= min_dist) {
            ShipAttack();
        }//endif
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

    void ShipAttack() {

        float GunVelo = ((agent.speed / 4) + 1) * 1024;

        if (GunVelo > 2048)
            GunVelo = 2048;

        for (int i = 0; i < AttackGuns.Length; i++)
        {
            //spawn an internal GameObject, to further manipulate with force addition, dependant on ship speed
            //code as is should be flexible, if/when rotation becomes an issue, later on.
            GameObject Bullet = Instantiate(Projectile, AttackGuns[i].transform.position, AttackGuns[i].transform.rotation) as GameObject;
            Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
            BulletRB.AddForce(AttackGuns[i].transform.forward * (GunVelo * 3 / 2));
            BulletRB.AddForce(AttackGuns[i].transform.up * (GunVelo / 2));
            //	Debug.Log ("Open Fire!");
        }
        FireRate = Max_FireRate;//reset fire rate
        Loaded = false;//attackcode
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

        //hack movement test, guess hack wise
        if (distance <= min_dist)
        {
            //agent.enabled = true;
            Vector3 offsetPlayer = target.position - transform.position;
            Vector3 dir = Vector3.Cross(offsetPlayer, Vector3.up);
            agent.SetDestination(transform.position + dir);
            //agent.SetDestination(target.position);//send to target destination
        }
        else
            agent.SetDestination(target.position);//send to target destination
        //endif
        //agent.enabled = false;
                                                    

        





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
            Score.ScoreCount(Kill_Value);//add value to death, test.
            Destroy(this.gameObject);//destroy an enemy
            Debug.Log("I'm sinking!!!");
        }//endif
    }
    //end of prototype damage code


    //begin attack code



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyScript : MonoBehaviour {

    public float HP_Max = 100F; //test prefab, unused assets are /*commented out*/
    public float /*HP_Sail, HP_Hull,*/ HP_Test; //test being there for testy purposes

	// Use this for initialization
	void Start () {
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
        else if (HP_Test <= 0)
            Destroy(this.gameObject);//destroy an enemy
        //endif
    }


}

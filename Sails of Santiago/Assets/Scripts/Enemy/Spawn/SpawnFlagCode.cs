using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFlagCode : MonoBehaviour {

    //enemy object/s to spawn
    public GameObject Enemy;

    public GameObject TheCode;
    private EnemySpawnerHack Ex_Code;
    private BoxCollider hitfield;

    public bool ColFlag = false;
    private void Start()
    {
        TheCode = GameObject.FindGameObjectWithTag("Spawner");
        Ex_Code = TheCode.GetComponent<EnemySpawnerHack>();
        hitfield = GetComponent<BoxCollider>();
    }


    public bool test = false;//test lazy code

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Terrain") { ColFlag = true; }
        else { ColFlag = false; }
    }

    public void FlagCheck() {
        if (!ColFlag) { SpawnShip(); Debug.Log("Reroll!"); }
        else { Ex_Code.SpawnAndCheck(); }//check again, if collider is true
    }


    private void SpawnShip() {
        Instantiate(Enemy, transform.position, transform.rotation);
    }

	// Use this for initialization void Start () {} // Update is called once per frame void Update () {}
}

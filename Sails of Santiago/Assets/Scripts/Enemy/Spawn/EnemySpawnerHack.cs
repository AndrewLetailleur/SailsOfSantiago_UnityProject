using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerHack : MonoBehaviour {

    //the objects, in lazy variables 
    //public GameObject Enemy;

    public GameObject flagger;
    public List<GameObject> spawnFlags = new List<GameObject>();
    public float angle_step = 0.1f;

    public int SpawnCount;//= GameObject.FindGameObjectsWithTag("Enemy").Length;
    public int MaxCount = 4;
    public float MaxTime = 10f;
    public float Counter = 4f;

    public float distance = 42f;//default dist, hack wise
    // float angle = Random.Range(-Mathf.PI, Mathf.PI);


    public Vector3 spawnPos;

    // Use this for initialization
    void Start() {
        SpawnCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Counter = MaxTime;

        float seaY = GameObject.FindGameObjectWithTag("Player").transform.position.y;

        float angle = 0;

        while (angle < 2 * Mathf.PI) {

            // calculate x, y from a vector with known length and angle
            float sx = distance * Mathf.Cos(angle);
            float sz = distance * Mathf.Sin(angle);
            
            spawnFlags.Add(Instantiate
                (flagger, new Vector3 (sx, seaY, sz),   //adds a new vector3, with the position co-ordinates
                transform.rotation,                     //keep rotation as is lazy, since it'd rotate itself
                transform.parent = this.transform)      //so that it's the 'child' from where this spawner is attached to
                as GameObject);                         //as the game object itself 

            angle += angle_step;
        }

        //if //
    }

    // Update is called once per frame
    void Update() {

        if (SpawnCount < MaxCount)
            Counter -= Time.deltaTime;
        //ENDIF, to bar 'debug spawn
        if (Counter <= 0.1F) {
            Counter = MaxTime;//reset timer
            SpawnCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (SpawnCount < MaxCount) { SpawnAndCheck(); }//fire a spawn flag, all systems go for firing a new enemy
            //else { Debug.Log("No More Foes"); }
        }
    }


    public void SpawnAndCheck()//get's a random spawner, and spawns a ship from there. Assuming said spawner doesn't collide with a 
    { 
        int i = Random.Range(0, spawnFlags.Count);//get between 0 array, and the max 'count' of spawn flags.
        SpawnFlagCode ex_code = spawnFlags[i].GetComponent<SpawnFlagCode>();
        ex_code.FlagCheck();//trigger external spawn flagger hack
    }
    
}

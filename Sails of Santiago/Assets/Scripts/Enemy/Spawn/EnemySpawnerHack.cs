using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerHack : MonoBehaviour {

    //the objects, in lazy variables 
    //public GameObject Enemy;

    public GameObject playRef;
    private float p_x, p_y, p_z;

    public GameObject flagger;
    public List<GameObject> spawnFlags = new List<GameObject>();
    public float angle_step = 0.1f;
    public bool PI_Half = true;
    private float PI_Angle = 160f;
    private float Player_Angle;
    private int PI_Multi;

    public int SpawnCount;//= GameObject.FindGameObjectsWithTag("Enemy").Length;
    public int MaxCount = 4;
    public float MaxTime = 10f;
    public float Counter = 4f;

    public float distance = 42f;//default dist, hack wise
    // float angle = Random.Range(-Mathf.PI, Mathf.PI);


    public Vector3 spawnPos;

    // Use this for initialization
    void Start() {
        playRef = GameObject.FindWithTag("Player");

        SpawnCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Counter = MaxTime;

        p_y = playRef.transform.position.y;



        if (PI_Half)
            PI_Multi = 1;
        else
            PI_Multi = 2;
        //endif

        float angle = 0;
        while (angle < PI_Multi * Mathf.PI) {//hack attempt at a "half circle" in placement

            // calculate x, y from a vector with known length and angle
            float sx = distance * Mathf.Cos(angle);
            float sz = distance * Mathf.Sin(angle);
            
            spawnFlags.Add(Instantiate
                (flagger, new Vector3 (sx, p_y, sz),   //adds a new vector3, with the position co-ordinates
                transform.rotation,                     //keep rotation as is lazy, since it'd rotate itself
                transform.parent = this.transform)      //so that it's the 'child' from where this spawner is attached to
                as GameObject);                         //as the game object itself 

            angle += angle_step;      //increment angle by amount of "steps". This should give approx 20 'steps' from a public float of 0.1F
        }

        if (PI_Half)
            transform.eulerAngles = new Vector3(0, PI_Angle, 0);
        //endif

        SetPosition();
    }


    void SetPosition() {
        p_x = playRef.transform.position.x;
        p_z = playRef.transform.position.z;
        this.transform.position = new Vector3(p_x, p_y, p_z);
    }

    // Update is called once per frame
    void Update() {
        SetPosition();

        if (PI_Half)
            SetSpawnRotation();

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

    void SetSpawnRotation() {
        Player_Angle = playRef.transform.eulerAngles.y;
        float spawnRot = PI_Angle + Player_Angle;
        transform.eulerAngles = new Vector3(0, spawnRot, 0);
    }


    public void SpawnAndCheck()//get's a random spawner, and spawns a ship from there. Assuming said spawner doesn't collide with a 
    { 
        int i = Random.Range(0, spawnFlags.Count);//get between 0 array, and the max 'count' of spawn flags.
        SpawnFlagCode ex_code = spawnFlags[i].GetComponent<SpawnFlagCode>();
        ex_code.FlagCheck();//trigger external spawn flagger hack
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testMoveScript : MonoBehaviour {

    private float MaxSpeed = 5;
    private float MinSpeed = 0;
    public float Speed = 1;

    public GameObject invisCursor;
    private NavMeshAgent agent;
    public Vector3 Local_dir, Cursor_dir, Move_vec;
    

	// Use this for initializatiison
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        Local_dir = this.transform.position;//get this position via transform

	}
	
	// Update is called once per frame
	void Update () {


        float i_C_X = invisCursor.transform.position.x;
        i_C_X += Time.deltaTime * Speed * (Input.GetAxis("Vertical"));
        float i_C_Y = invisCursor.transform.position.y;
        float i_C_Z = invisCursor.transform.position.z;
        i_C_Z += Time.deltaTime * Speed * (Input.GetAxis("Horizontal"));

        invisCursor.transform.position = new Vector3(i_C_X, i_C_Y, i_C_Z);
        Cursor_dir = invisCursor.transform.position;//get invis cursor location

        agent.destination = Cursor_dir;//the cursor direction



        /*
        if (Local_dir.x > MaxSpeed)
            Local_dir.x = MaxSpeed;
        else if (Local_dir.x < MinSpeed)
            Local_dir.x = MinSpeed;
        //endif
        if (Local_dir.z > MaxSpeed)     // as y. =/= height, whilst z =/= depth
            Local_dir.z = MaxSpeed;
        else if (Local_dir.z < MinSpeed)
            Local_dir.z = MinSpeed;
        //endif
        */


    }


}

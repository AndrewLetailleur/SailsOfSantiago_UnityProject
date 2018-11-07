using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidLimiters : MonoBehaviour {

    private GameObject refer;
    private Rigidbody RB;
    public float xz_limit = 25;
	// Use this for initialization
	void Start () {
        RB = this.GetComponent<Rigidbody>();
        refer = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {

        float cur_dir = refer.transform.rotation.y;
        float cur_x = refer.transform.rotation.x;
        float cur_z = refer.transform.rotation.z;
        Vector3 new_dir = new Vector3(cur_x, cur_dir, cur_z);
        if (cur_x >= 360 || cur_x <= -360)
            cur_x = 0;
        if (cur_z >= 360 || cur_z <= -360)
            cur_z = 0;

        if (cur_z < -0.1f) { 
            new_dir.z /= 2;
            if (cur_z < -50f)
                cur_z += 50f;
            //endif
        }
        else if (cur_z > 0.1f) { 
            new_dir.z /= 2;
            if (cur_z > 50f)
                cur_z -= 50f;
            //endif
        }//endif
        if (cur_x < -0.1f) { 
            new_dir.x /= 2;
            if (cur_z < -50f)
                cur_z += 50f;
            //endif
        }
        else if (cur_x > 0.1f) { 
            new_dir.x /= 2;
            if (cur_z > 50f)
                cur_z -= 50f;
            //endif
        }//endif

        this.transform.rotation = Quaternion.Euler(new_dir);
        //end hack attempt
    }
}

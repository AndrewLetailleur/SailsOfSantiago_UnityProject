using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirPushback : MonoBehaviour {

    public float pushback = 999;

    public bool playerIn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!playerIn)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log("Ship in test");
                playerIn = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        


        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player") {
            //Debug.Log("Ship still in");
            Rigidbody RB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 dir = other.transform.position - transform.position;//.normalized
            dir.Normalize();
            //            dir = -dir.normalized;
            RB.AddForce(dir * pushback);
        }
        

    }
}

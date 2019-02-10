using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

    public GameObject play;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        //offset = transform.position - play.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = play.transform.position;// + offset;
        transform.rotation = play.transform.rotation;
        //transform.LookAt(play.transform);
    }
}

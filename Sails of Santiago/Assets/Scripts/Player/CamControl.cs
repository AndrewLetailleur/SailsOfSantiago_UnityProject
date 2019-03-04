using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

    public GameObject play;
    private Vector3 offset;
    private PlayerShipControls_Script gamer;

	// Use this for initialization
	void Start () {
        //offset = transform.position - play.transform.position;
        gamer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShipControls_Script>();
	}

    // Update is called once per frame
    void Update() {

        if (gamer.HP_Hull > 0 && play != null) {
            transform.position = play.transform.position;
            transform.rotation = play.transform.rotation;// + offset;
        }
        else
            Destroy(play);
        //transform.LookAt(play.transform);
    }
}

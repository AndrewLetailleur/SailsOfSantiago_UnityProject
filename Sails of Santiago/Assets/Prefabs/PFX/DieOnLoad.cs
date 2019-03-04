using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnLoad : MonoBehaviour {

    int lifeSpan = 2;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, lifeSpan);
	}
	
}

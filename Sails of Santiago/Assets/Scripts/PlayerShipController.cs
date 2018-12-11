using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipController : MonoBehaviour, IShipController {

    [SerializeField]
    private float maxSpeed;
    private float currentSpeed;
    [SerializeField]
    private float turningDegreesPerSecond;
    [SerializeField]
    private float accellerationRate;
    [SerializeField]
    private float decellerationRate;
	
    private void Start() {
        currentSpeed = 0.0f;
    }

	// Update is called once per frame
	private void Update () {
        if (Input.GetAxis("Vertical") > 0) Accellerate();
        if (Input.GetAxis("Vertical") < 0) Decellerate();
        if (Input.GetAxis("Horizontal") > 0) TurnRight();
        if (Input.GetAxis("Horizontal") > 0) TurnLeft();
    }

    public void Accellerate() {
        //currentSpeed += accellerationRate * Time.deltaTime;
    }
    public void Decellerate() {
        //currentSpeed += decellerationRate * Time.deltaTime;
    }
    public void TurnLeft() {

    }
    public void TurnRight() {

    }
    public void AllStop() {

    }
}

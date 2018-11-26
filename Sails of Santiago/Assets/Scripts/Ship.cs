using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CapsuleCollider))]
public class Ship : MonoBehaviour, IShip {

    private string shipName = "SantiagoDefaultShipName";
    public string ShipName { get { return shipName; } }

    private float hullHealth;
    private float hullHealthMax;
    public float HullHealth { get { return hullHealth; } }
    private float sailHealth;
    private float sailHealthMax;
    public float SailHealth { get { return sailHealth; } }

    private CapsuleCollider interactionCollider;

    public Ship(string name) {
        if (name != null) shipName = name;
        hullHealthMax = 100.0f;
        hullHealth = hullHealthMax;
        sailHealthMax = 100.0f;
        sailHealth = sailHealthMax;
    }

    private void Awake() {
        interactionCollider = GetComponent<CapsuleCollider>();
    }

    public Transform GetTransform() {
        return transform;
    }

    public void Repair(float value) {

        if(hullHealth < hullHealthMax) {
            hullHealth += value;
        }
        else if ( hullHealth >= hullHealthMax) {
            hullHealth = hullHealthMax;
        }

        if (sailHealth < sailHealthMax) {
            sailHealth += value;
        }
        else if (sailHealth >= sailHealthMax) {
            sailHealth = sailHealthMax;
        }
    }

    public void Damage(float value, Collider c) {
        //logic for determining is damage is to sails or hull

        hullHealth -= value;
        sailHealth -= value;

        if (hullHealth <= 0.0f) Sink();
    }

    private void Sink() {
        //disable colliders etc, activate sinking animation.
        //Then delete gameobject
    }

}

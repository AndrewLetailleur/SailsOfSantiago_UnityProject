using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShip {

    string ShipName { get; }
    float HullHealth { get; }
    float SailHealth { get; }

    Transform GetTransform();

    void Repair(float value);
    void Damage(float value, Collider c);

}

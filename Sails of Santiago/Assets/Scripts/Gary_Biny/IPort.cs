using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPort {

    string PortName { get; }

    //Ports have a radius within which they will
    //be interactable and still
    //allow player repairs to be made.
    Transform PortAnchorPoint { get; }
    float RadiusOfInfluence { get; }

    //void ShipDeparting(IShip ship);
    //IEnumerator PassiveHealingInPort(IShip ship, IPort port, float radiusOfInfluence);
}

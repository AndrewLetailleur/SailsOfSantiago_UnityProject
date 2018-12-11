using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipController {

    void Accellerate();
    void Decellerate();
    void TurnLeft();
    void TurnRight();
    void AllStop();

}

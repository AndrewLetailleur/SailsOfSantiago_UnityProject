using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Port : MonoBehaviour, IPort {

    private string portName = "SantiagoDefaultPortName";
    public string PortName { get { return name; } }

    [SerializeField]
    private GameObject portInterface;
    [SerializeField]
    private Text interfaceTownName;

    //Where the area of influence is calculated from.
    [SerializeField]
    private Transform portAnchorPoint;
    public Transform PortAnchorPoint { get { return portAnchorPoint; } }
    private SphereCollider influenceSphere;

    //radius of the port's influence for interaction and repair.
    [SerializeField][Range(1, 100)]
    private float radiusOfInfluence;
    public float RadiusOfInfluence { get { return radiusOfInfluence; } }

    private List<IShip> shipsUnderRepair;

    public Port(string name) {
        if (name != null) portName = name;
    }

    //use this for gathering object, component and script references.
    private void Awake() {
        influenceSphere = GetComponent<SphereCollider>();
    }

    // Use this for variable initialization
    void Start() {
        interfaceTownName.text = name;
        influenceSphere.transform.position = portAnchorPoint.position;
        influenceSphere.radius = radiusOfInfluence * 0.75f;
        shipsUnderRepair = new List<IShip>();
    }

    private void OnTriggerEnter(Collider other) {
        IShip ship = other.GetComponent<IShip>();
        if (ship != null && !shipsUnderRepair.Contains(ship)) {
            shipsUnderRepair.Add(ship);
            StartCoroutine(PassiveHealingInPort(ship, this));
        }
        //if ships are made of multiple components may have to implement a
        //list and check if the ship is already repairing also.
        
    }

    //for crew repairing the ship themselves. Intended to be slow in final build.
    private IEnumerator PassiveHealingInPort(IShip ship, IPort port){

        Debug.Log("Repairs under way!");

        float distanceFromPort = (port.PortAnchorPoint.position 
            - ship.GetTransform().position).magnitude;

        while (distanceFromPort <= port.RadiusOfInfluence){

            //very small amount as it'll be applied on every frame. 
            ship.Repair(0.02f);

            distanceFromPort = (ship.GetTransform().position
            - port.PortAnchorPoint.position).magnitude;

            yield return null;
        }

        port.ShipDeparting(ship);
        Debug.Log("Repairs halted!");
    }

    public void ShipDeparting(IShip ship) {
        foreach (IShip s in shipsUnderRepair) {
            if (s == ship) shipsUnderRepair.Remove(s); break;
        }
    }
}

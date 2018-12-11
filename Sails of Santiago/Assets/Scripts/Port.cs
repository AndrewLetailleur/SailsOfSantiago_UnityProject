using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Port : MonoBehaviour, IPort, IInteractable {

    private string portName = "SantiagoDefaultPortName";
    public string PortName { get { return name; } }

    [SerializeField]
    private GameObject portInterface;
    [SerializeField]
    private GameObject diageticInterface;
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

    private List<IShip> shipsUnderInfluence;

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
        influenceSphere.radius = radiusOfInfluence * 0.75f; //buffer to stop immediate stopping.
        shipsUnderInfluence = new List<IShip>();
    }

    private void OnTriggerEnter(Collider other) {
        IShip ship = other.GetComponent<IShip>();
        if (ship != null && !shipsUnderInfluence.Contains(ship)) {
            shipsUnderInfluence.Add(ship);
            StartCoroutine(PassiveHealingInPort(ship, this));
        }
        
    }

    private void EnableDiageticUI() {
        diageticInterface.SetActive(true);
    }
    private void DisableDiageticUI() {
        diageticInterface.SetActive(false);
    }


    public void OnTriggerExit(Collider other) {

        IShip ship = other.GetComponent<IShip>();
        if(ship != null) {
            foreach (IShip s in shipsUnderInfluence) {
                if (s == ship) shipsUnderInfluence.Remove(s); break;
            }
        }

        if (other.CompareTag("Player")) DisableDiageticUI();

    }

    //NB: Coroutines may not be the best approach. Will use StatusEffect class etc in final build.
    //for crew repairing the ship themselves. Intended to be slow in final build.
    private IEnumerator PassiveHealingInPort(IShip ship, IPort port) {

        Debug.Log("Repairs under way!");

        float distanceFromPort = (port.PortAnchorPoint.position
            - ship.GetTransform().position).magnitude;

        while (distanceFromPort <= port.RadiusOfInfluence) {

            //very small amount as it'll be applied on every frame. 
            ship.Repair(0.02f);

            distanceFromPort = (ship.GetTransform().position
            - port.PortAnchorPoint.position).magnitude;

            yield return null;
        }

        //port.ShipDeparting(ship);
        Debug.Log("Repairs halted!");
    }





    //TODO Interface system reallyneeds to be implemented seperately, and used to request UI panels.
    public void Interact() {
        portInterface.SetActive(true);
    }

    public void CloseUI() {
        portInterface.SetActive(false);
    }
}

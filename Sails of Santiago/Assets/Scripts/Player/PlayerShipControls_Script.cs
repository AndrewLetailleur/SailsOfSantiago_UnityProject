using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipControls_Script : MonoBehaviour {


    private Rigidbody RB;//for easy ref of the Rigidbody
    public Slider HP_SLIDER; //for the slider GUI thing

    //gun code
    public GameObject Projectile;//the 'basic ball'
    public GameObject[] AttackGuns; //lazy hack test version, of the real deal
    //public GameObject[] LeftGuns, RightGuns, SpecialGuns;//gun array
        //rate of fire
    public float loadRate = 1F;//approx. 1 second or such, to start?

    //GUI Code
        //barf//
    //GunCode
        //barf//
    //StatusCode
        //DMG//
    //camera
        //see shark//
    //est?


    //for maximum health
    private int HP_MAX = 100;
    public float HP;
    //public Image ShipCON; // DamageFX
    //public Text GUI_HP; // To call upon later, HP Val to text

    //movement variables
        //momentum
    public float Velocity;
    public float MaxVelocity = 5f;
        //rotationary
    public float Rotation;
    public float MaxRotation = 5f;
        //acceleration
    public float Accel = 1f;
    public float Speed;



    //buggy variables, ONLY EDIT SCALE
    //asset prefabs
        public GameObject Sails;
        public Vector3 SailScale;//diff from Float, should take into account all the scale, for less hassle.
        public float Max_SY, Min_SY;
        private float differ;       //use for sail state?
        public bool Shrink = false;
        public bool Growth = false;
        //do not touch POS, it's a nightmare in positioning, glitch wise

    // Use this for initialization
    void Start() {

        //pre-empt set move to zero, caution wise.
        Rotation = 0;
        Velocity = 0;

        HP_SLIDER.maxValue = HP_MAX;
        HP = HP_SLIDER.maxValue;
        RB = this.GetComponent<Rigidbody>();//sets the RB

        SailScale = Sails.transform.localScale;
        Max_SY = SailScale.y;
        Min_SY = Max_SY / 20; //Scale of 1 / 20 = 0.05
        //
        differ = Max_SY - Min_SY;
        //get code to set array later

        //get ze guns
    }

    // Update is called once per frame
    void Update() {
        HP_SLIDER.value = HP;


        //vertical control
        if (Input.GetAxis("Vertical") < 0)
            Speed -= Time.deltaTime * MaxVelocity / 2;//fast break hack
        else // if > 0
            Speed += Time.deltaTime * (Input.GetAxis("Vertical") * Accel);
        //endif

        //vertical velocity
        if (Speed >= MaxVelocity)
            Speed = MaxVelocity;
        else if (Speed <= 0) {
            Speed = 0;
        }
        //end if
        Velocity = RB.velocity.magnitude;

        //vertical control
        MoveShip();
        //horizontal control
        RotateShip();

        //buggy pirate flag code. Now with only toscale woes!
        ChangeSails();

        //combat code
        ShipAttack();
    }

    void ShipAttack()
    {
        //Spacebar for now, BAR test-y wise

        if (Input.GetKeyDown("space")) {//think of how to dodge a fore loop?
            for (int i = 0; i < AttackGuns.Length; i++) {
                //spawn an internal GameObject, to further manipulate with force addition, depending on current knot speed.
                //this can also be additionally useful, if/when rotation becomes an issue, later on.
                GameObject Bullet = Instantiate(Projectile, AttackGuns[i].transform.position, AttackGuns[i].transform.rotation) as GameObject;
                Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
                BulletRB.AddForce(transform.forward * (1024));
                BulletRB.AddForce(transform.up * (1024));
                //	Debug.Log ("Open Fire!");
            }//end for    //C_Load();//reload, disabled for test code

        }
    }



    //Ship Velocity
    void MoveShip() {
        RB.AddForce(transform.forward * Speed);
        if (RB.velocity.magnitude > MaxVelocity) {
            RB.velocity = RB.velocity.normalized * MaxVelocity;
        }//not perfect, but it will do by force


        if (RB.velocity.magnitude < 0.5F && Speed == 0) {
            RB.velocity = Vector3.zero;
        }

    }
    void RotateShip() {
        Rotation = (Input.GetAxis("Horizontal") * Time.deltaTime * (Velocity + 3) );

        if (Rotation >= MaxRotation)
            Rotation = MaxRotation;
        else if (Rotation <= -MaxRotation)
            Rotation = -MaxRotation;
        //endif

        transform.Rotate(0, Rotation, 0);//works, but is too sharp
        //think of how to then rotate the force/momentum of the ship
            
        Vector3 velocity = RB.velocity;//TEMP VALUE, SOURCE == https://answers.unity.com/questions/10781/how-might-i-rotate-rigidbody-momentum.html
        RB.velocity = Vector3.zero;
        RB.velocity = transform.forward * velocity.magnitude;//good enough for now
    }

    
    


    //STILL Buggy right now. Fix later.
    void ChangeSails() {
        if (Shrink) {
            if ((SailScale.y - 0.1f <= Min_SY))
                SailScale.y = Min_SY;
            else 
                SailScale.y =- 0.1f;//(Time.deltaTime / 10);
        }//endif

        if (Growth) {
            if ((SailScale.y + 0.1f >= Max_SY))
                SailScale.y = Max_SY;
            else
                SailScale.y =+ 0.1f;//(Time.deltaTime / 10);
        }//endif

        Sails.transform.localScale = SailScale;//the rescaling. Do NOT touch position.
    }

        

} 
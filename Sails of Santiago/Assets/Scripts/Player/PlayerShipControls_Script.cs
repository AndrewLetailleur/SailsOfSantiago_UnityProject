using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShipControls_Script : MonoBehaviour {


    private Rigidbody RB;//for easy ref of the Rigidbody
    public Slider HP_SLIDER; //for the slider GUI thing

    //gun code
    public GameObject Projectile;//the 'basic ball'
    private GameObject[] AttackGuns; //lazy hack test version, of the real deal
    //public GameObject[] LeftGuns, RightGuns, SpecialGuns;//gun array
        //rate of fire
    public float loadRate = 1F;//approx. 1 second or such, to start?
    private float a_load = 0f;//loading times, reset upon fire
        //left
        //right
        //spec
    private bool canFire = true;//debug, cue firing pins.
        //left
        //right
        //spec

    //GUI Code
        //barf//
    //GunCode
        //barf//
    //StatusCode
        //DMG//
    //camera
        //see shark//
    //est?

        //GUI variables
    //for maximum health    , curr version works with slider feature ATM
    private float HP_MAX = 100f;//int for now, san's sake
    private float HP;//needs to be accessed for now.
        //rely on tags instead, to call it private?
    //public Text GUI_HP; // To call upon later, HP Val to text
        //public Image ShipCON; // DamageFX

    //movement variables
    //momentum
    private float Velocity;
    private float MinVelocity = 0f; //private, since min HAS to be set at Zero
    public float CurMaxVelocity;
    public float MaxVelocity = 25f;
        //rotationary
    private float Rotation;
    public float MaxRotation = 5f;
        //acceleration
    public float Accel = 1f;
    public float Speed;


        //ship sail variables, WIP
    //buggy variables, ONLY EDIT SCALE
    //asset prefabs
        public GameObject Sails;
        public Vector3 SailScale;//diff from Float, should take into account all the scale, for less hassle.
        public float Max_SY, Min_SY;
        private float differ;       //use for sail state?
        private bool Shrink, Growth = false;
        //do not touch POS, it's a nightmare in positioning, glitch wise

    // Use this for initialization
    void Start() {

        
        //set sail's pre-emptively est.
        //set up the projectiles, update auto wiser.
        AttackGuns = GameObject.FindGameObjectsWithTag("P_SpecCannon");

        //pre-empt set move to zero, caution wise.
        Rotation = 0;
        Velocity = 0;

        HP_SLIDER.maxValue = HP_MAX;
        HP = HP_SLIDER.maxValue;
        RB = this.GetComponent<Rigidbody>();//sets the RB

        SailScale = Sails.transform.localScale; //get the scale of the actual transforms
        Max_SY = SailScale.y;
        Min_SY = Max_SY / 20; //Scale of 1 / 20 = 0.05

        //set to zero, as there's no movement.
        SailScale.y = Min_SY;
        Sails.transform.localScale = SailScale;
        //
        differ = Max_SY - Min_SY;
        //get code to set array later
        //get ze guns
    }

    // Update is called once per frame
    void Update() {
            //update GUI, later when harmed only to save on RAM.
        HP_SLIDER.value = HP;
        
        //vertical control
        MoveShip();
        //horizontal control
        RotateShip();

        //buggy pirate flag code. Now with only toscale woes!
        ChangeSails();

        //combat code
        ShipAttack();
            //check firing pin's afterwards
        Reload();
            //the GUI
        //GuiCode();

    }
    
    //Ship Velocity
    void MoveShip()
    {
        Speed += Time.deltaTime * (Input.GetAxis("Vertical") * Accel);

        //vertical control
        if (Input.GetAxis("Vertical") < 0)
        {
            Growth = false;
            Shrink = true;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            Growth = true;
            Shrink = false;
        }
        else { 
            Growth = false;
            Shrink = false;
        }

        //vertical velocity
        if (Speed >= MaxVelocity)
            Speed = MaxVelocity;
        else if (Speed >= CurMaxVelocity + 10)
            Speed -= Time.deltaTime * Accel; //to drag, == MaxVelocity;
        else if (Speed >= CurMaxVelocity)
            Speed -= Time.deltaTime; //to drag, == MaxVelocity;
        else if (Speed <= 0)
            Speed = 0;
        //end if
        Velocity = RB.velocity.magnitude;

        //part 2,momentum
        RB.AddForce(transform.forward * (Speed * Accel));
        if (RB.velocity.magnitude > MaxVelocity)
            RB.velocity = RB.velocity.normalized * MaxVelocity;
        //endif - not perfect, but it will do by force

        if (RB.velocity.magnitude < 0.1F && Speed == 0)
        {
            RB.velocity = Vector3.zero;
        }

    }


    //Shift the sails, visual effect.
    void ChangeSails()
    {
        if (Shrink) {
            if ((SailScale.y - Time.deltaTime <= Min_SY))
                SailScale.y = Min_SY;
            else
                SailScale.y -= Time.deltaTime;//(Time.deltaTime / 10);
        }//endif 
        if (Growth) {
            if ((SailScale.y + Time.deltaTime >= Max_SY))
                SailScale.y = Max_SY;
            else
                SailScale.y += Time.deltaTime;//(Time.deltaTime / 10);
        }//endif 
        Sails.transform.localScale = SailScale;//the rescaling. Do NOT touch position.

        //max speed dependant on scale size
        differ = Max_SY - Min_SY;
        if (SailScale.y - Min_SY >= (differ / 4 * 3)) 
            CurMaxVelocity = differ * 20;
        else if (SailScale.y - Min_SY >= (differ / 2))
            CurMaxVelocity = differ * 10;
        else
            CurMaxVelocity = differ * 5;
        //endif



    }


    void RotateShip()
    {
        //horizontal control
        Rotation = (Input.GetAxis("Horizontal") * Time.deltaTime * (Velocity + Accel));
            //goes to "0" if no input is given. Sharp[y]
        if (Rotation >= MaxRotation)
            Rotation = MaxRotation;
        else if (Rotation <= -MaxRotation)
            Rotation = -MaxRotation;
        //endif

        //the rotation magic
        transform.Rotate(0, Rotation, 0);//works, but is too sharp
                                         //think of how to then rotate the force/momentum of the ship

        //rotate the velocity as well, after rotating.
        Vector3 velocity = RB.velocity;//TEMP VALUE, SOURCE == https://answers.unity.com/questions/10781/how-might-i-rotate-rigidbody-momentum.html
        RB.velocity = Vector3.zero;
        RB.velocity = transform.forward * velocity.magnitude;//good enough for now
    }

    void ShipAttack()
    {
        float GunVelo = ((Speed / 4) + 1) * 1024;
         
        if (GunVelo > 2048)
            GunVelo = 2048;
        //end hack if
        //Spacebar for now, BAR test-y wise

        if (Input.GetKeyDown("space") && canFire) {//think of how to dodge a fore loop?
            for (int i = 0; i < AttackGuns.Length; i++) {
                //spawn an internal GameObject, to further manipulate with force addition, depending on current knot speed.
                //this can also be additionally useful, if/when rotation becomes an issue, later on.
                GameObject Bullet = Instantiate(Projectile, AttackGuns[i].transform.position, AttackGuns[i].transform.rotation) as GameObject;
                Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
                BulletRB.AddForce(transform.forward * (GunVelo * 3 / 2));
                BulletRB.AddForce(transform.up * (GunVelo / 2));
                //	Debug.Log ("Open Fire!");
            }//end for    //C_Load();//reload, disabled for test code
            canFire = false;
            a_load = loadRate;//reset the firing pin
        }
    }


    void Reload() {
        if (!canFire) {
            a_load -= Time.deltaTime;
            if (a_load <= 0)
                canFire = true;
            //endif
        }
    }
        

} 
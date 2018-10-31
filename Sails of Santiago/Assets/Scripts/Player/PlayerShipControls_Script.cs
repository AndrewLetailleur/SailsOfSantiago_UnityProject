using System;//for math functionality, to the power of
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;//needed for navmesh components
/* Code Written by; Andrew Letailleur (2018) */

public class PlayerShipControls_Script : MonoBehaviour {

    //general rule of code here; MAX values are not private, for easy editing. General values are however, private.
        //only exception is assets that are a hassle to link otherwise; in which case they're assigned public

    ///GUI & Stats code, later link/grab dependencies from upgrades
        //GUI Sliders/est
    private Slider LEFT_SLIDE, RIGHT_SLIDE, SPEC_SLIDE; //for the slider GUI thing
        //health and est/ammo values, stat wise
    public float HP_MAX = 100f;//set at 100, and floaty for maximum compatibility on percentages
    private float Sail_A, HP_Sail, Hull_A, HP_Hull; //to edit the Alpha values of images, and acquire/set the health of those assets
        //GUI stuff
    private Image Sail_HUD, Hull_HUD;//for the GUI images, edit wise
    private Color Sail_COL, Hull_COL;//to grab the colors, alpha edit wise
    private Text Hull_TXT, Sail_TXT, Spec_TXT; //for the text display, GUI edit wise

    ///ship sail variables, (mostly) var less buggy now edition!
    private GameObject Sails;//public, as find objects with tag ain't reliable for finding ONLY stuff inside an object, say.
    public Vector3 SailScale;//diff from Float, should take into account all the scale, for less hassle.
    private float Max_SY, Min_SY, Max_SZ, Min_SZ, differ, diff_Z;//get sail heightness & thickness, along with sail difference state
    private bool Shrink, Growth = false;//is it rising/falling triggers
        //do not touch POS/itioning of the sails themselves; as that's a nightmare in technical difficulties, glitch wise

    //gun code, deals with firing and attack grabs
    public GameObject Projectile;//the 'basic ball'
    private GameObject[] AttackGuns, LeftGuns, RightGuns; //lazy hack test version, of the real deal. Attack =/= Special, carry over wise
    private bool canFire, leftFire, rghtFire = true;//fire triggers, cue firing pins.
        //rate of fire
    private float loadRate = 1F;//approx. 1 second or such, to start?
    private float s_load, l_load, r_load = 0f;//loading times, reset upon fire
        //special ammo
    private int MaxAmmo = 21;//prefab test for now, to ensue there's special ammo in toe. Should be int, bar floaty timer shenanigans with dragon fire?
    private int SpecAmmo;//the current amount of ammo
    

    /// <summary> Later variables when tinkering is allowed. Mainly future proofing
    /// //StatusCode
    ///    //DMG//
    /// //camera
    ///    //see shark//
    /// //est?
    /// </summary>
    
    
    //movement code, as we have to get transport/est from somewhere. Ideally, should push momentum, not transport an object.

        //movement variables
    private float Velocity; //current velocity
    private float MinVelocity = 0.1f; //private, since min HAS to be set at Zero. Hindsight, mag 
    private float CurMaxVelocity;//actual max velocity, dependant on sail health.
    public float MaxVelocity = 25f;//THE Max velocity, made public for easy access/edit by inspector.
        //rotationary
    private float Rotation;
    public float MaxRotation = 5f;//THE Max rotation, made public for easy access/edit by inspector.
        //acceleration
    public float Accel = 1f;
    public float Speed;//same as Max Rotation and Velocity, yet kept public for now for easy reference.
        //misc physics, tinker wise
    private Rigidbody RB;//for easy ref of the Rigidbody
    //end of player variables

    // Use this for initialization
    void Start() {
        
        /// <summary> newer hack attempt on more efficient grabbing object. Reference/s;
        /// https://answers.unity.com/questions/787538/how-do-i-find-a-child-object-in-a-hierarchy-of-chi.html
        /// </summary>
        Transform[] SailCheck = GetComponentsInChildren<Transform>();
        foreach (Transform check in SailCheck) {
            if (check.CompareTag("SailPoint")) {
                Sails = check.gameObject; //add the check as game object, hack test
            }
        }//end foreach
        //end nicely efficient get that SailCheck
 
        //set up the projectiles, update auto wiser. FindWithTag works in this case, since it's linked only to the player itself.
        AttackGuns = GameObject.FindGameObjectsWithTag("P_SpecCannon");
        LeftGuns = GameObject.FindGameObjectsWithTag ("P_LeftCannon");
        RightGuns = GameObject.FindGameObjectsWithTag("P_RightCannon");
        //pre-empt set move to zero, caution wise.
        Rotation = 0; Velocity = 0;

        //get sliders, then set values est
            //left cannon set up
        l_load = loadRate;
        LEFT_SLIDE = GameObject.FindGameObjectWithTag("LeftAmmoGUI").GetComponent<Slider>();//\\\
        LEFT_SLIDE.maxValue = loadRate;
        LEFT_SLIDE.value = CalcLeftLoad();
            //right cannon set up
        r_load = loadRate;
        RIGHT_SLIDE = GameObject.FindGameObjectWithTag("RightAmmoGUI").GetComponent<Slider>();//|| don't forget to GET the component on top
        RIGHT_SLIDE.maxValue = loadRate;
        RIGHT_SLIDE.value = CalcRightLoad();
            //special ability set up
        s_load = loadRate;
        SPEC_SLIDE = GameObject.FindGameObjectWithTag("SpecialGUI").GetComponent<Slider>();//
        SPEC_SLIDE.maxValue = loadRate;
        SPEC_SLIDE.value = CalcSpecialLoad();
        //text HUD variables on top
        SpecAmmo = MaxAmmo;//auto load
        Spec_TXT = GameObject.FindGameObjectWithTag("SpecialTXT").GetComponent<Text>();
        Spec_TXT.text = "Ammo: " + SpecAmmo + "/" + MaxAmmo;
        //end grab sliders

        //get GUI on Hull and Sail, then set up accordingly.
            //hull
        HP_Hull = HP_MAX; //max is 100, for 100%
        Hull_TXT = GameObject.FindGameObjectWithTag("HullTXT").GetComponent<Text>();
        Hull_HUD = GameObject.FindGameObjectWithTag("HullGUI").GetComponent<Image>();
        Hull_COL = Hull_HUD.color;
        Hull_Update();
            //sail
        HP_Sail = HP_MAX; //max is 100, for 100%
        Sail_HUD = GameObject.FindGameObjectWithTag("SailGUI").GetComponent<Image>();
        Sail_TXT = GameObject.FindGameObjectWithTag("SailTXT").GetComponent<Text>();
        Sail_COL = Sail_HUD.color;
        Sail_Update();
        //end GUI set & update on health variables

            //finally uses/assigns the rigid body onto RB, for RigidBodyness.
        RB = this.GetComponent<Rigidbody>();//sets the RB
            //get the scale of the actual transforms
        SailScale = Sails.transform.localScale; //this code calcs, assuming the sails are fully down at the start.
            //y height var
        Max_SY = SailScale.y;
        Min_SY = Max_SY / 20; //Scale of 1 / 20 = 0.05
        differ = Max_SY - Min_SY;
            //z width var
        Min_SZ = SailScale.z;
        Max_SZ = Min_SZ * 4; // scale of 1 / 4, from sails unfurled.
        diff_Z = Max_SZ - Min_SZ;
            //set sail scale
        SailScale.y = Min_SY;//set to zero, as there's no movement.
        SailScale.z = Max_SZ;//that means, sails are all rolled up and padded
        Sails.transform.localScale = SailScale;
        //end set up
    }


    // Update is called once per frame
    void Update() {
            //update GUI, later when harmed only to save on RAM.
        
        //vertical control
        MoveShip();
        //horizontal control
        RotateShip();
        //buggy pirate flag code. Now with only toscale woes!
        ChangeSails();
        //combat code
        ShipAttack();//check firing pin's afterwards
        //the GUI
        GUICode();//also does reloading, so there's no need TO reload :P
            //testground for stuff
        //TestVoider();
    }

    //damage call codes, for script reference. Decreases health by X amount.
        //for ports, if lazy/hacky. Use the same code... But do Negative Values instead. As two '-'s =/= a +, operator wise
    public void SailDamage(float damage) {//by amount
        HP_Sail -= damage; //take away by amount
        Sail_Update();
    }
    public void HullDamage(float damage) {//by amount
        HP_Hull -= damage; //take away by amount
        Hull_Update();
    }//once Damage is calc'd, call upon a GUI update for the values, graphics wise.

    /*void TestVoider() {///test prefabs, to check hp updates work
        if (Input.GetKeyDown(KeyCode.K)) {
            HP_Sail -= 1.14f;
            Sail_Update();
        } if (Input.GetKeyDown(KeyCode.L)) {
            HP_Hull -= 9.1f;
            Hull_Update();
        }
    }*/ ///end test prefabs
    /*collision code, ported over to externals, for easier editing/referencing, dependency/collision wise.
    private void OnTriggerEnter(Collider other){}
    private void OnCollisionEnter(Collision collision){}*/ ///end prototype of collision code
    
        //update health variables if hit/est
    void Hull_Update()
    {
        if (HP_Hull > HP_MAX) { HP_Hull = HP_MAX; }//jnc trigger
        //endif
        Hull_A = (HP_Hull / 100); //to get the 1f minimum
        if (Hull_A < 0) { Hull_A = 0; }//so it doesn't go above 0
        //end if, consider adding a "if above 1" variable as a consideration later
        Hull_COL.a = Hull_A; //set transparency, should be at 1 approx.
        if (HP_Hull <= 0)
            Hull_TXT.text = "SUNK!";//then trigger sink death con?
        else
            Hull_TXT.text = "Hull:" + Math.Round(HP_Hull, 3) + "%";//rounds the digits down, JNC
        //endif

        Hull_HUD.color = Hull_COL;
    }
    void Sail_Update()
    {
        if (HP_Sail > HP_MAX) { HP_Sail = HP_MAX; }//jnc trigger
        //endif
        Sail_A = (HP_Sail / 100); //to get the 1f minimum
        if (Sail_A < 0) { Sail_A = 0; }//so it doesn't go above 0
        //end if, consider adding a "if above 1" variable as a consideration later
        Sail_COL.a = Sail_A; //set transparency, should be at 1 approx.
        if (HP_Sail <= 0)
            Sail_TXT.text = "DOWN!";
        else
            Sail_TXT.text = "Sail:" + Math.Round(HP_Sail, 3) + "%";//rounds the digits down, JNC
        //endif

        //update SailSpeed, say?
        Sail_HUD.color = Sail_COL;
    }
    //end damage code
    
        ///begin ship movement code

    //Ship Velocity
    void MoveShip()
    {
        //sets Speed to input, plus Velocity.
        Speed += Time.deltaTime * (Input.GetAxis("Vertical") * Accel);

        //vertical control
        if (Input.GetAxis("Vertical") < 0) { Growth = false; Shrink = true; }
        else if (Input.GetAxis("Vertical") > 0) { Growth = true; Shrink = false; }
        else { Growth = false; Shrink = false; }

        //vertical velocity
        if (Speed >= MaxVelocity)
            Speed = MaxVelocity;
        else if (Speed >= CurMaxVelocity + 10)
            Speed -= Time.deltaTime * Accel; //to drag, == MaxVelocity;
        else if (Speed >= CurMaxVelocity)
            Speed -= Time.deltaTime; //to drag, == MaxVelocity;
        else if (Speed <= MinVelocity) //if speed is below this minimum velocity.
            Speed = 0;
        //end if

        //set velocity to current magnitute in velocity
        Velocity = RB.velocity.magnitude;

        //part 2,momentum
        RB.AddForce(transform.forward * (Speed * Accel));
        if (RB.velocity.magnitude > MaxVelocity) { RB.velocity = RB.velocity.normalized * MaxVelocity; }
        //endif - not perfect, but it will do by force
        if (RB.velocity.magnitude < MinVelocity && Speed == 0) { RB.velocity = Vector3.zero; }
        //endif, zero velocity trigger condition
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
        transform.Rotate(0, Rotation, 0);//works, but is too sharp. Think of how to then rotate the force/momentum of the ship

        //rotate the velocity as well, after rotating.
        Vector3 velocity = RB.velocity;//TEMP VALUE, SOURCE == https://answers.unity.com/questions/10781/how-might-i-rotate-rigidbody-momentum.html
        RB.velocity = Vector3.zero;
        RB.velocity = transform.forward * velocity.magnitude;//good enough for now
    }

    //Shift the sails, visual effect.
    void ChangeSails()
    {
        if (Shrink) {
            if ((SailScale.y - Time.deltaTime <= Min_SY)) {
                SailScale.y = Min_SY;
                SailScale.z = Max_SZ;
            } else {
                SailScale.y -= Time.deltaTime;//(Time.deltaTime / 10);
                if (SailScale.z + (Time.deltaTime) <= Max_SZ)
                    SailScale.z += (Time.deltaTime); //hack test, unthicken
                else
                    SailScale.z = Max_SZ;
                //end if checker
            }
        }//endif 
        if (Growth) {
            if ((SailScale.y + Time.deltaTime >= Max_SY)) { 
                SailScale.y = Max_SY;
                SailScale.z = Min_SZ;
            } else { 
                SailScale.y += Time.deltaTime;//(Time.deltaTime / 10);
                if (SailScale.z - (Time.deltaTime) >= Min_SZ)
                    SailScale.z -= (Time.deltaTime); //hack test, unthicken
                else
                    SailScale.z = Min_SZ;
                //end if checker
            }
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
        
    }//end sails
    //end movement code
    
    //begin attack code
    void ShipAttack()
    {
        float GunVelo = ((Speed / 4) + 1) * 1024;
         
        if (GunVelo > 2048)
            GunVelo = 2048;
        //end hack if
        //Spacebar for now, BAR test-y wise

            //firing code "works" for now, but needs some fine tuning both on code end, and asset end. Pointer wise.
        if (Input.GetKeyDown("space") && canFire) {//think of how to dodge a fore loop?
            for (int i = 0; i < AttackGuns.Length; i++) {
                //spawn an internal GameObject, to further manipulate with force addition, depending on current knot speed.
                //this can also be additionally useful, if/when rotation becomes an issue, later on.
                GameObject Bullet = Instantiate(Projectile, AttackGuns[i].transform.position, AttackGuns[i].transform.rotation) as GameObject;
                Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
                BulletRB.AddForce(AttackGuns[i].transform.forward * (GunVelo * 3 / 2));
                BulletRB.AddForce(AttackGuns[i].transform.up * (GunVelo / 2));
                //	Debug.Log ("Open Fire!");
            }//end for    //C_Load();//reload, disabled for test code
            S_Load();
        }

        if (Input.GetKeyDown("z") && leftFire)
        {//think of how to dodge a fore loop?
            for (int i = 0; i < LeftGuns.Length; i++)
            {
                //spawn an internal GameObject, to further manipulate with force addition, depending on current knot speed.
                //this can also be additionally useful, if/when rotation becomes an issue, later on.
                GameObject Bullet = Instantiate(Projectile, LeftGuns[i].transform.position, LeftGuns[i].transform.rotation) as GameObject;
                Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
                BulletRB.AddForce(LeftGuns[i].transform.forward * (GunVelo * 3 / 2));
                BulletRB.AddForce(LeftGuns[i].transform.up * (GunVelo / 2));
                //	Debug.Log ("Open Fire!");
            }//end for    //C_Load();//reload, disabled for test code
            L_Load();
            leftFire = false;
        }

        if (Input.GetKeyDown("c") && rghtFire)
        {//think of how to dodge a fore loop?
            for (int i = 0; i < RightGuns.Length; i++)
            {
                //spawn an internal GameObject, to further manipulate with force addition, depending on current knot speed.
                //this can also be additionally useful, if/when rotation becomes an issue, later on.
                GameObject Bullet = Instantiate(Projectile, RightGuns[i].transform.position, RightGuns[i].transform.rotation) as GameObject;
                Rigidbody BulletRB = Bullet.GetComponent<Rigidbody>();
                BulletRB.AddForce(RightGuns[i].transform.forward * (GunVelo * 3 / 2));
                BulletRB.AddForce(RightGuns[i].transform.up * (GunVelo / 2));
                //	Debug.Log ("Open Fire!");
            }//end for    //C_Load();//reload, disabled for test code
            R_Load();
            rghtFire = false;
        }
    }

    void GUICode() {
        //ammo part
        if (!canFire /*&& (SpecAmmo > 0)*/) {//if special bar is empty, disabled for now for testing purposes, as it works.
            s_load += Time.deltaTime;
            if (s_load >= loadRate) {
                //if (!canFire)
                //    SpecAmmo--;
                //endif
                canFire = true;
                s_load = loadRate;
                Spec_TXT.text = "Ammo: " + SpecAmmo + "/" + MaxAmmo;
            }
            SPEC_SLIDE.value = CalcSpecialLoad();
        }
        if (!leftFire) {//if left bar is empty
            l_load += Time.deltaTime;
            if (l_load >= loadRate) {
                leftFire = true;
                l_load = loadRate;
            }
            LEFT_SLIDE.value = CalcLeftLoad();
        }
        if (!rghtFire) {//if right bar is empty
            r_load += Time.deltaTime;
            if (r_load >= loadRate) {
                rghtFire = true;
                r_load = loadRate;
            }
            RIGHT_SLIDE.value = CalcRightLoad();
        }
    }

    //sets firing conditions to false, and empty sliders of each bar
    void S_Load()
    {//begin calc return valves for loading variables
        canFire = false;
        s_load = 0;
        SPEC_SLIDE.value = CalcSpecialLoad();
    }
    float CalcSpecialLoad()
    {
        return s_load;
    } //end firing trig code
    void L_Load()
    {//begin calc return valves for loading variables
        leftFire = false;
        l_load = 0;
        LEFT_SLIDE.value = CalcLeftLoad();
    }
    float CalcLeftLoad()
    {
        return l_load;
    }
    void R_Load()
    {//begin calc return valves for loading variables
        rghtFire = false;
        r_load = 0;
        RIGHT_SLIDE.value = CalcRightLoad();
    }
    float CalcRightLoad()
    {
        return r_load;
    }

 

} 
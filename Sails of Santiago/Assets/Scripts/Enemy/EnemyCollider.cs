using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour {

    public GameObject hitbox;/*set as linked to parent, hack wiser*/
    public BaseEnemyScript baseRef;
    public float damage = 10f; //test variable
    //public bool Saily, Hully = false;/*test triggers, not used atm*/

    // Use this for initialization
    void Start () {
        //get parent test hitbox = this.gameObject.GetComponentInParent;

        //begin longwinded hack to find a parent, that I've a feeling could be massively simplified. But don't know how, so this should hack wise work.
        //based in reference to this article: https://docs.unity3d.com/ScriptReference/Transform-parent.html
        if (this.gameObject.transform.parent.parent.parent.parent.parent != null)
            hitbox = this.gameObject.transform.parent.parent.parent.parent.parent.gameObject;
        else if (this.gameObject.transform.parent.parent.parent.parent != null)
            hitbox = this.gameObject.transform.parent.parent.parent.parent.gameObject;
        else if (this.gameObject.transform.parent.parent.parent != null)
            hitbox = this.gameObject.transform.parent.parent.parent.gameObject;
        else if (this.gameObject.transform.parent.parent != null)
            hitbox = this.gameObject.transform.parent.parent.gameObject;
        else if (this.gameObject.transform.parent != null)
            hitbox = this.gameObject.transform.parent.gameObject;
        else /*out of things to search for parent wise*/
            hitbox = this.gameObject;
        //end convoluted IF tree Mk2

            //after the long ass tree, hack wise
            baseRef = hitbox.GetComponent<BaseEnemyScript>();//grab the script
        }

            private void OnTriggerEnter(Collider other)
            {
                if (other.gameObject.tag == "PlayerShot") /*test prefab for now*/
                {
                    Debug.Log("Enemy is damaged!!!");
                    baseRef.TestDamage(damage);
                    Destroy(other.gameObject);//destroy the collider afterwards, jnc. AFTOMH, doesn't even work.
                }

            }
        }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARO : MonoBehaviour
{

    public float range;
    string TargetTeam;
    // Start is called before the first frame update
    void Start()
    {
        SphereCollider atkRadius = GetComponent<SphereCollider>();
        atkRadius.radius = range;
        atkRadius.isTrigger = true;
        TargetTeam = this.gameObject.GetComponentInParent<Unit>().TargetTeam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
 *  When a potential target enters range (and there is no current target) attack the target
 */
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == TargetTeam && tag == "Player")
        {
            //Debug.Log("TARGET: ", target);
        }
        //TODO: Please for the love find a more effecient way to do this tim. It hurts my soul
        if (this.GetComponentInParent<Unit>().target == null && other.tag == TargetTeam)
        {
            this.GetComponentInParent<Unit>().target = other.GetComponent<Unit>();
        }
    }

    /*
     * If there is no target and there is another potential target within range, then attack that target
     */
    private void OnTriggerStay(Collider other)
    {
        if (this.GetComponentInParent<Unit>().target == null && other.tag == TargetTeam)
        {
            this.GetComponentInParent<Unit>().target = other.GetComponent<Unit>();
        }
    }

    /*
     * Deactivates the target allowing for new targets to be selected:
     * 
     * TODO: Add a flag that causes unit to pursue the target if expressly chosen by player
     */
    private void OnTriggerExit(Collider other)
    {
        if (other == this.GetComponentInParent<Unit>().target)
        {
            this.GetComponentInParent<Unit>().target = null;
        }
    }
}

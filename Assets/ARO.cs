using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARO : MonoBehaviour
{

    public float range;
    Unit unit;
    GameManager.Teams TargetTeam;

    // Start is called before the first frame update
    void Start()
    {
        SphereCollider atkRadius = GetComponent<SphereCollider>();
        atkRadius.radius = range;
        atkRadius.isTrigger = true;
        unit = gameObject.GetComponentInParent<Unit>();

        if(unit.Team == GameManager.Teams.Enemy)
        {
            TargetTeam = GameManager.Teams.Player;
        }
        else
        {
            TargetTeam = GameManager.Teams.Enemy;
        }
        
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
        MotherShip mother = other.GetComponent<MotherShip>();
        if(mother != null && mother.Team == TargetTeam)
        {
            unit.MotherTarget = mother;
            unit.target = null;
            return;
        }

        Unit other_unit = other.GetComponent<Unit>();
        if(other_unit != null && other_unit.Team == TargetTeam && unit.target == null)
        {
            unit.target = other_unit;
        }
    }

    /*
     * If there is no target and there is another potential target within range, then attack that target
     */
    private void OnTriggerStay(Collider other)
    {
        Unit other_unit = other.GetComponent<Unit>();
        if (other_unit != null && other_unit.Team == TargetTeam && unit.target == null && unit.MotherTarget == null)
        {
            unit.target = other.GetComponent<Unit>();
        }
    }

    /*
     * Deactivates the target allowing for new targets to be selected:
     * 
     * TODO: Add a flag that causes unit to pursue the target if expressly chosen by player
     */
    private void OnTriggerExit(Collider other)
    {
        MotherShip mother = other.GetComponent<MotherShip>();
        if (mother != null && mother == unit.MotherTarget)
        {
            unit.MotherTarget = null;
            return;
        }

        Unit other_unit = other.GetComponent<Unit>();
        if (other_unit != null && other_unit == unit.target)
        {
            unit.target = null;
        }
    }
}

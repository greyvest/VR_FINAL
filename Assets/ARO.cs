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
        
        unit = gameObject.GetComponentInParent<Unit>();
        atkRadius.radius = unit.GetStats.Range;
        atkRadius.isTrigger = true;

        if (unit.Team == GameManager.Teams.Enemy)
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
        if(unit.hasTarget)
        {
            return;
        }

        MotherShip mother = other.GetComponent<MotherShip>();
        if (mother != null && mother.Team == TargetTeam)
        {
            unit.MotherTarget = mother;
            unit.hasTarget = true;
            unit.AttackTarget();
            return;
        }

        Unit other_unit = other.GetComponent<Unit>();
        if(other_unit != null && other_unit.Team == TargetTeam)
        {
            unit.hasTarget = true;
            unit.target = other_unit;
            other_unit.unitDead.AddListener(unit.TargetDead);
            unit.AttackTarget();
        }
    }

    /*
     * If there is no target and there is another potential target within range, then attack that target
     */
    private void OnTriggerStay(Collider other)
    {
        Unit other_unit = other.GetComponent<Unit>();
        if (other_unit == null)
        {
            return;
        }

        else if (unit.hasTarget)
        {
            if(other_unit == unit.target)
                unit.AttackTarget();
        }
        else if(other_unit.Team == TargetTeam)
        {
            unit.hasTarget = true;
            unit.target = other_unit;
            other_unit.unitDead.AddListener(unit.TargetDead);
            unit.AttackTarget();
        }
    }

    /*
     * Deactivates the target allowing for new targets to be selected:
     * 
     * TODO: Add a flag that causes unit to pursue the target if expressly chosen by player
     */
    private void OnTriggerExit(Collider other)
    {
        if (!unit.hasTarget)
        {
            return;
        }
        else if (unit.MotherTarget != null)
        {
            MotherShip mother = other.GetComponent<MotherShip>();
            if (mother != null && mother == unit.MotherTarget)
            {
                unit.MotherTarget = null;
                unit.hasTarget = false;
                return;
            }
        }
        else if (unit.target != null)
        {
            Unit other_unit = other.GetComponent<Unit>();
            if (other_unit != null && other_unit == unit.target)
            {
                unit.target = null;
                unit.hasTarget = false;
                other_unit.unitDead.RemoveListener(unit.TargetDead);
            }
        }
    }
}

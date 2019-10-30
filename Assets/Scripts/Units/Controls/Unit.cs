using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SphereCollider))]
public class Unit : MonoBehaviour
{
    [Tooltip("The tag of the opposing team")]
    public string TargetTeam;

    [SerializeField]
    UnitScriptableObject stats;
    NavMeshAgent agent;

    Unit target;
    float HP;
    bool cooldown;
    bool pursue;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.Speed;

        SphereCollider atkRadius = GetComponent<SphereCollider>();
        atkRadius.radius = stats.Range;
        atkRadius.isTrigger = true;

        HP = stats.MaxHealth;

        cooldown = false;
        pursue = false;

        target = null;
    }


    /*
     * NAVMESH tool to traverse the level 
     */ 
    public void TravelTo(Vector3 point)
    {
        agent.SetDestination(point);
    }


    /*
     *  When a potential target enters range (and there is no current target) attack the target
     */ 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Other: " + other.tag + " other: ", other);
        if(target == null && other.tag == TargetTeam)
        {
            Debug.Log("Target AQUIRED");
            target = other.GetComponent<Unit>();
        }
    }

    /*
     * If there is no target and there is another potential target within range, then attack that target
     */ 
    private void OnTriggerStay(Collider other)
    {
        if(target == null && other.tag == TargetTeam)
        {
            target = other.GetComponent<Unit>();
        }
    }

    /*
     * Deactivates the target allowing for new targets to be selected:
     * 
     * TODO: Add a flag that causes unit to pursue the target if expressly chosen by player
     */ 
    private void OnTriggerExit(Collider other)
    {
        if(other == target)
        {
            target = null;
        }
    }


    /*
     * Activates cooldown flag to prevent units from firing between cooldowns
     * deals damage to the target
     * resets the cooldown flag to false after the atk speed duration has passed
     */ 
    IEnumerator Fire()
    {
        cooldown = true;
        target.TakeDamage(stats.Damage);
        yield return new WaitForSeconds(stats.AtkSpeed);
        cooldown = false;
    }

    /*
     * decreases the Unit's HP by dmg. Destroys the unit if HP <= 0
     */ 
    public void TakeDamage(float dmg)
    {
        HP -= dmg;
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Unit unit)
    {
        target = unit;
        pursue = true;
    }

    private void Update()
    {
        
        //If we should pursue the target, and they are more than half our radius away, then move towards them
        if (pursue && Vector3.Distance(target.transform.position, transform.position) > stats.Range / 2)
        {
            TravelTo(target.transform.position);
        }

        //Every update, if cooldown is on firing is over, there is a target, and that target is within range, then attack the target
        if (!cooldown && target != null && Vector3.Distance(target.transform.position, transform.position) < stats.Range)
        {
            StartCoroutine(Fire());
        }

        // if there is no target and pursue is true then set to false
        if (target == null && pursue)
        {
            pursue = false;
        }
    }
}

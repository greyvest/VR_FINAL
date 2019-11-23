using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

public class UnitEvent : UnityEvent<Unit> { }

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(NavMeshObstacle))]
public class Unit : MonoBehaviour
{
    public GameManager.Teams Team;

    public enum unitType { Small, Medium, Large};

    public unitType uType;

    private GameObject childAtkRadiusObject;

    UnitEvent unitDead;

    [SerializeField]
    UnitScriptableObject stats;
    LineRenderer laser;
    NavMeshAgent agent;

    public Unit target;
    float HP;
    bool cooldown;
    bool pursue;

    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.Speed;

        laser = GetComponent<LineRenderer>();
        laser.enabled = false;

        childAtkRadiusObject = GetComponentInChildren<ARO>().gameObject;



        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        HP = stats.MaxHealth;

        cooldown = false;
        pursue = false;

        target = null;

        unitDead = new UnitEvent();
        unitDead.AddListener(AIController.Instance.UnRegisterUnit);
        AIController.Instance.RegisterUnit(this);
    }

    public Vector3 MovingTo()
    {
        return destination;
    }


    /*
     * NAVMESH tool to traverse the level 
     */ 
    public void TravelTo(Vector3 point)
    {
        agent.SetDestination(point);
        destination = point;
    }





    /*
     * Activates cooldown flag to prevent units from firing between cooldowns
     * deals damage to the target
     * resets the cooldown flag to false after the atk speed duration has passed
     */ 
    IEnumerator Fire()
    {
        cooldown = true;
        laser.enabled = true;
        for (int i = 0; i < 5; i++)
        {
            laser.SetPosition(0, transform.position);
            laser.SetPosition(1, target.transform.position);
            yield return new WaitForSeconds(.1f);
        }
        target.TakeDamage(stats.Damage);
        laser.enabled = false;
        yield return new WaitForSeconds(stats.AtkSpeed - .5f);
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
            unitDead.Invoke(this);
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

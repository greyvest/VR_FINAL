﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Events;

public class UnitEvent : UnityEvent<Unit> { }

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour
{
    public GameManager.Teams Team;

    public enum unitType { Small, Medium, Large };

    public unitType uType;

    private GameObject childAtkRadiusObject;

    public UnitEvent unitDead;

    [SerializeField]
    UnitScriptableObject stats;
    NavMeshAgent agent;

    public UnitScriptableObject GetStats { get { return stats; } }

    public Unit target;
    public MotherShip MotherTarget;
    float HP;
    bool cooldown;
    bool pursue;

    public bool hasTarget;
    Vector3 destination;

    [SerializeField]
    GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.Speed;

        childAtkRadiusObject = GetComponentInChildren<ARO>().gameObject;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;

        HP = stats.MaxHealth;

        cooldown = false;
        pursue = false;
        hasTarget = false;

        target = null;
        MotherTarget = null;

        unitDead = new UnitEvent();
        unitDead.AddListener(AIController.Instance.UnRegisterUnit);
        AIController.Instance.RegisterUnit(this);
    }

    public Vector3 MovingTo()
    {
        return destination;
    }

    public void TargetDead(Unit u)
    {
        target = null;
        pursue = false;
        hasTarget = false;
    }
    /*
     * NAVMESH tool to traverse the level 
     */ 
    public void TravelTo(Vector3 point)
    {
        pursue = false;
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
        if(MotherTarget != null)
        {
            MotherTarget.TakeDamage(stats.Damage);
        }
        else if (target != null)
        {
            target.TakeDamage(stats.Damage);
        }
        
        yield return new WaitForSeconds(stats.AtkSpeed);
        cooldown = false;
    }

    public void AttackTarget()
    {
        if(!cooldown)
        {
            StartCoroutine(Fire());
        }
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
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void SetTarget(Unit unit)
    {
        target = unit;
        pursue = true;
        hasTarget = true;
        unit.unitDead.AddListener(TargetDead);
        TravelTo(unit.transform.position);
    }

    public void SetTarget(MotherShip mother)
    {
        target = null;
        pursue = false;
        hasTarget = true;
        TravelTo(mother.transform.position);
    }

    private void Update()
    {

        if (pursue && Vector3.Distance(target.transform.position, transform.position) > stats.Range / 2)
        {
            TravelTo(target.transform.position);
        }
    }
}

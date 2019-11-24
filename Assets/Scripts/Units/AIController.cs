using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public static AIController Instance;

    List<Unit> EnemyUnits;
    List<Unit> PlayerUnits;

    [SerializeField]
    MotherShip PlayerMother;
    [SerializeField]
    MotherShip EnemyMother;
    
       
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        EnemyUnits = new List<Unit>();
        PlayerUnits = new List<Unit>();
    }

    public void RegisterUnit(Unit u)
    {
        if(u.Team == GameManager.Teams.Enemy)
        {
            EnemyUnits.Add(u);
        }
        else
        {
            PlayerUnits.Add(u);
        }
    }

    public void UnRegisterUnit(Unit u)
    {
        if (u.Team == GameManager.Teams.Enemy)
        {
            EnemyUnits.Remove(u);
        }
        else
        {
            PlayerUnits.Remove(u);
        }
    }


    // Update is called once per frame
    void Update()
    {
        foreach (Unit unit in EnemyUnits)
        {
            ChooseTarget(unit);
        }
    }

    private void ChooseTarget(Unit unit)
    {

    }
}

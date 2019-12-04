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
    System.Random rng;

    [SerializeField,Range(0,10)]
    int MotherBias = 3;
       
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
        rng = new System.Random();
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
    bool start = true;
    bool temp = true;

    IEnumerator TEST()
    {
        yield return new WaitForSeconds(10);
        temp = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach (Unit unit in EnemyUnits)
        {
            Vector3 position = unit.transform.position;
            foreach(Unit player in PlayerUnits)
            {
                if(Vector3.Distance(player.transform.position, unit.transform.position) <= unit.GetStats.Range/2)
                {
                    Vector3 difference = player.transform.position - unit.transform.position;
                    position -= difference;
                }
            }
            if (position == unit.transform.position)
            {
                unit.TravelTo(PlayerMother.transform.position);
            }
            else
            {
                unit.TravelTo(position);
            }
        }
        */
        if(temp)
        {
            if (start)
            {
                start = false;
                StartCoroutine(TEST());
            }
            return;
        }
        foreach (Unit unit in EnemyUnits)
        {
            if (!unit.hasTarget)
            {
                int targetIndex = rng.Next(0, PlayerUnits.Count + MotherBias);
                if (targetIndex >= PlayerUnits.Count)
                {
                    unit.SetTarget(PlayerMother);
                }
                else
                {
                    unit.SetTarget(PlayerUnits[targetIndex]);
                }
            }
        }
    }
}

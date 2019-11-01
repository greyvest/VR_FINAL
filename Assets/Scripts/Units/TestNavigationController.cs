using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavigationController : MonoBehaviour
{
    public Unit player;
    public Transform[] points;
    public Unit[] Enemies;
    public Vector3[] targets;
    // Start is called before the first frame update
    void Start()
    {
        targets = null;
    }

    

    // Update is called once per frame
    void Update()
    {
        /*
        if(targets == null)
        {
            targets = new Vector3[Enemies.Length];
            for (int i = 0; i < Enemies.Length; i++)
            {
                int r = Random.Range(0, Enemies.Length);
                //Debug.Log(r);
                targets[i] = points[r].position;
                Debug.Log("EnemiesPos: " + Enemies[i].transform.position);
                Debug.Log("EnemiesTarget" + targets[i]);
                Enemies[i].TravelTo(targets[i]);
            }
        }
        */
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            player.TravelTo(points[0].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            player.TravelTo(points[1].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            player.TravelTo(points[2].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            player.TravelTo(points[3].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            player.TravelTo(points[4].position);
        }

        /*
        for (int i = 0; i < Enemies.Length; i++)
        {
            if(Enemies[i].GetComponent<NavMeshAgent>().remainingDistance <= 0.01f)//.velocity.magnitude <= .5f)
            {
                targets[i] = points[Random.Range(0, points.Length - 1)].position;
                Enemies[i].TravelTo(targets[i]);
            }
        }
        */
    }
}

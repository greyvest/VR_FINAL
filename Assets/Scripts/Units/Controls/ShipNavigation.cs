using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipNavigation : MonoBehaviour
{

    [SerializeField]
    NavMeshAgent agent;

    Vector3 TargetPos;
    bool fighting = false;

    public void TravelTo(Vector3 point)
    {
        //TargetPos = point;
        agent.SetDestination(point);
    }


    private IEnumerator PauseBeforeKill(GameObject enemy)
    {
        yield return new WaitForSeconds(3);
        Destroy(enemy);
        fighting = false;
        agent.isStopped = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name);
        if(!fighting && other.tag == "Enemy")
        {
            fighting = true;
            agent.isStopped = true;
            StartCoroutine(PauseBeforeKill(other.gameObject));
        }
    }
}

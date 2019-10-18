using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(SphereCollider))]
public class PlayerUnit : MonoBehaviour
{
    [SerializeField]
    UnitScriptableObject stats;
    SphereCollider range;
    NavMeshAgent agent;

    //Vector3 TargetPos;
    bool fighting;
    float CurrentHealth;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.Speed;

        range = GetComponent<SphereCollider>();
        range.radius = stats.Range;
        range.isTrigger = true;

        CurrentHealth = stats.MaxHealth;

        fighting = false;
    }

    /* This method sends the player unit to the position specified by point*/
    public void TravelTo(Vector3 point)
    {
        agent.SetDestination(point);
    }

    /* Used to inflict damage to the playerunit */
    public void TakeDamage(float dmg)
    {
        CurrentHealth -= dmg;
        if(CurrentHealth <= 0f)
        {
            Debug.Log("UNIT DIED" + name);
            Destroy(gameObject);
        }
    }

    /* WORK IN PROGRESS, REQUIRES SOME DISCUSSION OF
     * GAMEPLAY ETC.
     * 
     * When the player unit comes within range of the enemy it will stop and attack
     * the enemy and then continue to its destination
     */
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.name);
        if (!fighting && other.tag == "Enemy")
        {
            fighting = true;
            agent.isStopped = true;
            StartCoroutine(AttackEnemy(other.gameObject));
        }
    }

    /* PLACE HOLDER TO PREVENT UNIT FROM BEING STUCK AT PLACE FOREVER
     * EVENTUALLY, WILL DO THE FOLLOWING:
     *  -Every x duration inflict dmg to enemy until dead (or interuppted)
     * 
     * NOTE: Need to figure out what exactly the stopping behaviour of units should be
     */
    IEnumerator AttackEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(3f);
        Destroy(enemy);
        fighting = false;
        agent.isStopped = false;
    }
}

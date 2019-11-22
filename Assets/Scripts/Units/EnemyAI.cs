using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    Unit unit;
    [SerializeField]
    float avoidDistance = 3f;
    [SerializeField]
    MotherShip target;

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (unit.MovingTo() != target.transform.position)
        {
            unit.TravelTo(target.transform.position);
        }
    }
}

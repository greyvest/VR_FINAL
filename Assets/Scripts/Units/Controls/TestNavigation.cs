using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNavigation : MonoBehaviour
{
    [SerializeField]
    Transform[] points = new Transform [5];
    [SerializeField]
    ShipNavigation ship;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            ship.TravelTo(points[0].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ship.TravelTo(points[1].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ship.TravelTo(points[2].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            ship.TravelTo(points[3].position);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            ship.TravelTo(points[4].position);
        }
    }
}

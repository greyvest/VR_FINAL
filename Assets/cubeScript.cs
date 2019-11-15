using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class cubeScript : MonoBehaviour
{
    List<GameObject> currentlySelectedRaw;

    // Start is called before the first frame update
    void Start()
    {
        currentlySelectedRaw = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        currentlySelectedRaw = new List<GameObject>();
    }

    public List<GameObject> getFilteredSelection(int num)
    {
        List<GameObject> returnList = new List<GameObject>();

        switch(num)
        {
            //No filters
            case 0:
                return currentlySelectedRaw;
            //only small
            case 1:
                addSmall(returnList);
                return returnList;
            //only medium
            case 2:
                addMedium(returnList);
                return returnList;
            //only large
            case 3:
                addLarge(returnList);
                return returnList;
            //small and large
            case 4:
                addSmall(returnList);
                addLarge(returnList);
                return returnList;
            //small and medium
            case 5:
                addSmall(returnList);
                addMedium(returnList);
                return returnList;
            //medium and large
            case 6:
                addMedium(returnList);
                addLarge(returnList);
                return returnList;

            default:
                return currentlySelectedRaw;
                
        }


    }

    private void addLarge(List<GameObject> returnList)
    {
        foreach (GameObject x in currentlySelectedRaw)
        {
            if (x.GetComponent<Unit>().uType == Unit.unitType.Large)
            {
                returnList.Add(x);
            }
        }
    }

    private void addMedium(List<GameObject> returnList)
    {
        foreach (GameObject x in currentlySelectedRaw)
        {
            if (x.GetComponent<Unit>().uType == Unit.unitType.Medium)
            {
                returnList.Add(x);
            }
        }
    }

    private void addSmall(List<GameObject> returnList)
    {
        foreach (GameObject x in currentlySelectedRaw)
        {
            if (x.GetComponent<Unit>().uType == Unit.unitType.Small)
            {
                returnList.Add(x);
            }
        }
    }

    //When the square collides with a ship, add the ship to the raw selection
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Selection collided with object");
        if(other.gameObject.tag == "Player")
        {
            currentlySelectedRaw.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Selector collided with object");
    }

    //When the square shrinks and does not collide with ship, remove ship from from raw selection
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            currentlySelectedRaw.Remove(other.gameObject);
        }
    }
}

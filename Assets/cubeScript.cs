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

    public List<GameObject> getFilteredSelection(bool small, bool medium)
    {
        List<GameObject> returnList = new List<GameObject>();
        if (small)
        {
            
            if (medium)
            {
                addLarge(returnList);
            }
            else
            {
                Debug.Log("Adding small to returnlist");
                addSmall(returnList);
            }
        }
        else if (medium)
        {
            addMedium(returnList);
        }
        else
        {
            returnList = currentlySelectedRaw;
        }
        //Should not trigger
        return returnList;

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
        Debug.Log("Adding small");
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
        if(other.gameObject.tag == "Player")
        {
            currentlySelectedRaw.Add(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVRTouchSample;
using OVR;
using System;


public class InputManager : MonoBehaviour
{

    //Variables
    Vector3 boxOneStart;
    Vector3 boxOneEnd;
    Vector3 boxTwoStart;
    Vector3 boxTwoEnd;

    GameObject selectionPane1;
    GameObject selectionPane2;

    public GameObject righthand;
    public GameObject lefthand;

    SelectionManager SelectionManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("40");
        }
        //Primary controller detection
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("TriggerPull");
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.beginSquareR(hit.point);
                }
            }
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Trigger is down");
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.updateSquareR(hit.point);
                }
            }

        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Released Trigger");
            checkFlags();
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.fireSquareR(hit.point);
                }
            }

        }
        //Secondary controller detection
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("TriggerPull 2");
            RaycastHit hit;
            if (Physics.Raycast(lefthand.transform.position, lefthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.beginSquareL(hit.point);
                }
            }
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Trigger is down 2");
            RaycastHit hit;
            if (Physics.Raycast(lefthand.transform.position, lefthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.updateSquareL(hit.point);
                }
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Released Trigger 2");
            RaycastHit hit;
            if (Physics.Raycast(lefthand.transform.position, lefthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.fireSquareL(hit.point);
                }
            }

        }

    }

    //This should check to see which face buttons are held down for filtering purposes
    private void checkFlags()
    {
        throw new NotImplementedException();
    }
}



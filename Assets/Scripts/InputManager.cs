﻿using System.Collections;
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

    public GameObject selectionPane1;
    public GameObject selectionPane2;

    public GameObject righthand;
    public GameObject lefthand;

    public GameObject startPointPrefab;
    public GameObject startPointParent;

    public SelectionManager SM;

    public GameObject combatPlane;
    int layerMask = 1 << 8;

    GameObject startPointMarker;
    GameObject midPointMarker;
    GameObject endPointMarker;

    GameObject startPointMarker2;
    GameObject midPointMarker2;
    GameObject endPointMarker2;

    private List<GameObject> rSelection;
    private List<GameObject> lSelection;

    public LineRenderer laserLineRenderer;
    public LineRenderer laserLineRenderer2;


    // Start is called before the first frame update
    void Start()
    {
        laserLineRenderer.startWidth = .01f;
        laserLineRenderer.startColor = Color.red;
        laserLineRenderer2.startWidth = .01f;
        laserLineRenderer2.startColor = Color.cyan;

        Debug.Log(LayerMask.LayerToName(8));
    }

    // Update is called once per frame
    void Update()
    {
        #region draw debug ray
        // Draw a debug ray from the right hand. 
        Ray ray = new Ray(righthand.transform.position, righthand.transform.forward);
        RaycastHit raycastHit;
        Vector3 endPosition = righthand.transform.position + (60 * righthand.transform.forward);

        if (Physics.Raycast(ray, out raycastHit, 60))
        {
            endPosition = raycastHit.point;
        }

        laserLineRenderer.SetPosition(0, righthand.transform.position);
        laserLineRenderer.SetPosition(1, endPosition);

        // Draw a debug ray from the left hand. 
        Ray ray2 = new Ray(lefthand.transform.position, lefthand.transform.forward);
        RaycastHit raycastHit2;
        Vector3 endPosition2 = lefthand.transform.position + (60 * lefthand.transform.forward);

        if (Physics.Raycast(ray2, out raycastHit2, 60))
        {
            endPosition2 = raycastHit2.point;
        }

        laserLineRenderer2.SetPosition(0, lefthand.transform.position);
        laserLineRenderer2.SetPosition(1, endPosition2);
        #endregion draw debug ray



        if (OVRInput.Get(OVRInput.Button.Start))
        {
            Application.LoadLevel(Application.loadedLevel);
        }


        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("40");
        }


        //Trigger pull on primary controller detection
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            rSelection = new List<GameObject>();
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    startPointMarker = Instantiate(startPointPrefab, hit.point, combatPlane.transform.rotation);
                    midPointMarker = Instantiate(startPointPrefab, hit.point, combatPlane.transform.rotation);
                    selectionPane1.SetActive(true);
                }
            }
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    midPointMarker.transform.position = hit.point;
                    Vector3 center = (hit.point + startPointMarker.transform.position) / 2f;

                    selectionPane1.transform.position = center;
                    //new Vector3(center.x, center.y -1 , center.z);


                    float sizex = Mathf.Abs(startPointMarker.transform.position.x - hit.point.x);
                    float sizez = Mathf.Abs(startPointMarker.transform.position.z - hit.point.z);


                    selectionPane1.transform.localScale = new Vector3(sizex, selectionPane1.transform.localScale.y, sizez);



                }
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            selectionPane1.gameObject.SetActive(false);
            selectionPane1.transform.localScale = new Vector3(1, 1, 1);

            rSelection = selectionPane1.GetComponent<cubeScript>().getFilteredSelection(checkFlags());
            Debug.Log(rSelection[0].gameObject.name);
            Destroy(startPointMarker);
            Destroy(midPointMarker);
        }


        //Secondary controller detection
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            lSelection = new List<GameObject>();

            RaycastHit hit;

            if (Physics.Raycast(lefthand.transform.position, lefthand.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    startPointMarker2 = Instantiate(startPointPrefab, hit.point, combatPlane.transform.rotation);
                    midPointMarker2 = Instantiate(startPointPrefab, hit.point, combatPlane.transform.rotation);
                    selectionPane2.SetActive(true);
                }
            }
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(lefthand.transform.position, lefthand.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    midPointMarker2.transform.position = hit.point;
                    Vector3 center = (hit.point + startPointMarker2.transform.position) / 2f;

                    selectionPane2.transform.position = center;
                    //new Vector3(center.x, center.y -1 , center.z);


                    float sizex = Mathf.Abs(startPointMarker2.transform.position.x - hit.point.x);
                    float sizez = Mathf.Abs(startPointMarker2.transform.position.z - hit.point.z);



                    selectionPane2.transform.localScale = new Vector3(sizex, selectionPane2.transform.localScale.y, sizez);



                }
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            selectionPane2.gameObject.SetActive(false);
            selectionPane2.transform.localScale = new Vector3(1, 1, 1);
            lSelection = selectionPane2.GetComponent<cubeScript>().getFilteredSelection(checkFlags());

            Destroy(startPointMarker2);
            Destroy(midPointMarker2);
        }

        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log("Hand trigger");
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                foreach (GameObject unit in rSelection)
                {
                    Debug.Log("Asking unit " + unit.gameObject.name + "to move to " + hit.point);
                    unit.GetComponent<Unit>().TravelTo(hit.point);
                }
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(lefthand.transform.position, lefthand.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                foreach (GameObject unit in lSelection)
                {
                    unit.GetComponent<Unit>().TravelTo(hit.point);
                }
            }

        }

    }

    

    //This should check to see which face buttons are held down for filtering purposes
    private int checkFlags()
    {
        //TODO: Implement this based on buttons
        return 0;
    }
}



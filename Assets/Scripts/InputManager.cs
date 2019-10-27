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

    public GameObject selectionPane1;
    public GameObject selectionPane2;

    public GameObject righthand;
    public GameObject lefthand;

    public GameObject startPointPrefab;
    public GameObject startPointParent;

    public GameObject combatPlane;
    int layerMask = 1 << 8;

    GameObject startPointMarker;
    GameObject midPointMarker;
    GameObject endPointMarker;
    GameObject topCornerMarker;
    GameObject bottomCornerMarker;

    public LineRenderer laserLineRenderer;

    SelectionManager SelectionManager;

    // Start is called before the first frame update
    void Start()
    {
        laserLineRenderer.startWidth = .01f;
        laserLineRenderer.startColor = Color.red;
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
        #endregion draw debug ray


        if (OVRInput.Get(OVRInput.Button.One))
        {
            Debug.Log("40");
        }
        //Trigger pull on primary controller detection
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {

            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit, Mathf.Infinity ,layerMask))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    startPointMarker = Instantiate(startPointPrefab, hit.point, combatPlane.transform.rotation);
                    midPointMarker = Instantiate(startPointPrefab, hit.point, combatPlane.transform.rotation);
                    selectionPane1.SetActive(true);
                    Debug.Log("Spawned marker at " + hit.point);
                }
            }
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit, Mathf.Infinity,layerMask))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    midPointMarker.transform.position = hit.point;
                    Vector3 center = (hit.point + startPointMarker.transform.position) / 2f;

                    selectionPane1.transform.position = center;
                        //new Vector3(center.x, center.y -1 , center.z);


                    float sizex = Mathf.Abs(startPointMarker.transform.position.x - hit.point.x);
                    float sizez = Mathf.Abs(startPointMarker.transform.position.z - hit.point.z);


                    Debug.Log("SizeX:" + sizex + "   SizeZ: " + sizez + "   Hit point" + hit.point);

                    selectionPane1.transform.localScale = new Vector3(sizex, selectionPane1.transform.localScale.y, sizez);

                    

                }
            }
           

            //            selectionPane1.transform.rotation = new Quaternion(-Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z, Camera.main.transform.rotation.w);

            /*RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.updateSquareR(hit.point);
                }
            }*/

        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
    //        Debug.Log("Released Trigger");
            selectionPane1.gameObject.SetActive(false);
            selectionPane1.transform.localScale = new Vector3(1, 1, 1);
            Destroy(startPointMarker);
            Destroy(midPointMarker);
            /*
            checkFlags();
            RaycastHit hit;
            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.fireSquareR(hit.point);
                }
            }*/

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



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

    GameObject startPointMarker;
    GameObject midPointMarker;
    GameObject endPointMarker;
    GameObject topCornerMarker;
    GameObject bottomCornerMarker;

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
  //          Debug.Log("TriggerPull");
            selectionPane1.gameObject.SetActive(true);

            selectionPane1.transform.LookAt(selectionPane1.transform.position + Camera.main.transform.rotation * Vector3.forward,
 Camera.main.transform.rotation * Vector3.up);

            Vector3 faceCam = Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.up);
            Vector3 faceCam2 = Vector3.Cross(Camera.main.transform.forward, Camera.main.transform.right);

            Vector3 vR = Camera.main.transform.localRotation.eulerAngles;
            vR.y *= -1;

            selectionPane1.transform.rotation = new Quaternion(faceCam.x, faceCam.y, faceCam.z, 1);

            boxOneStart = righthand.transform.position;

            startPointMarker = Instantiate(startPointPrefab, boxOneStart, new Quaternion(0, 0, 0, 0));

            midPointMarker = Instantiate(startPointPrefab, boxOneStart, new Quaternion(0, 0, 0, 0));
            /*
            RaycastHit hit;

            if (Physics.Raycast(righthand.transform.position, righthand.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("SelectionPlane"))
                {
                    SelectionManager.beginSquareR(hit.point);
                }
            }*/
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
//            Debug.Log("Trigger is down");
            if (selectionPane1.gameObject.activeSelf == false)
            {
                selectionPane1.gameObject.SetActive(true);
            }

            Vector3 center = (righthand.transform.position + boxOneStart) / 2f;
            Debug.Log(boxOneStart);
            Debug.Log(righthand.transform.position);
            Debug.Log(center);
            Debug.Log(center);
            float sizex = Mathf.Abs(boxOneStart.x - righthand.transform.position.x);
            float sizey = Mathf.Abs(boxOneStart.y - righthand.transform.position.y);

            midPointMarker.transform.position = center;
            selectionPane1.transform.position = center;

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



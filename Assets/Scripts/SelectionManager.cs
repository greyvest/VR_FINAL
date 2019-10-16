using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    //Much of this logic was gleaned from this video https://www.youtube.com/watch?v=vsdIhyLKgjc

    Vector3 rightHandStartPoint;
    Vector3 leftHandStartPoint;
    Vector3 rightHandEndPoint;
    Vector3 leftHandEndPoint;

    //
    private GameObject selectSquareImageR;
    private GameObject selectSquareImageL;

    public List<GameObject> selectedObjectsR;
    public List<GameObject> selectedObjectsL;

    private enum filters {Big, Medium, Small};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Beginning click and drag per controller
    public void beginSquareR(Vector3 point)
    {
        rightHandStartPoint = point;
    }
    public void beginSquareL(Vector3 point)
    {
        leftHandStartPoint = point;
    }

    //Maintaining and updating click and drag every click
    internal void updateSquareR(Vector3 point)
    {
        if (selectSquareImageR.gameObject.activeInHierarchy)
        {
            selectSquareImageR.gameObject.SetActive(true);
        }
        rightHandEndPoint = point;
        Vector3 center = (rightHandStartPoint + rightHandEndPoint / 2f);
        float sizex = Mathf.Abs(rightHandStartPoint.x - rightHandEndPoint.x);
        float sizey = Mathf.Abs(rightHandStartPoint.y - rightHandEndPoint.y);

        selectSquareImageR.transform.position = center;
        selectSquareImageR.transform.lossyScale.Set(sizex, sizey, selectSquareImageR.transform.lossyScale.z);

    }

    internal void updateSquareL(Vector3 point)
    {
        if (selectSquareImageR.gameObject.activeInHierarchy)
        {
            selectSquareImageR.gameObject.SetActive(true);
        }
        rightHandEndPoint = point;
        Vector3 center = (rightHandStartPoint + rightHandEndPoint / 2f);
        float sizex = Mathf.Abs(rightHandStartPoint.x - rightHandEndPoint.x);
        float sizey = Mathf.Abs(rightHandStartPoint.y - rightHandEndPoint.y);

        selectSquareImageR.transform.position = center;
        selectSquareImageR.transform.lossyScale.Set(sizex, sizey, selectSquareImageR.transform.lossyScale.z);
    }


    //Ending click and drag and running functions to select everything inside of it
    internal void fireSquareR(Vector3 point)
    {
        selectSquareImageR.SetActive(false);
        findSelectedObjetsR();

    }

    internal void fireSquareL(Vector3 point)
    {
        selectSquareImageR.SetActive(false);
        findSelectedObjetsL();

    }


    private void findSelectedObjetsR()
    {
        Collider[] hitColliders = Physics.OverlapBox(selectSquareImageR.transform.position, selectSquareImageR.transform.localScale / 2, Quaternion.identity);
        foreach (Collider x in hitColliders)
        {
            selectedObjectsR.Add(x.gameObject);
        }
    }

    private void findSelectedObjetsL()
    {
        Collider[] hitColliders = Physics.OverlapBox(selectSquareImageL.transform.position, selectSquareImageL.transform.localScale / 2, Quaternion.identity);
        foreach (Collider x in hitColliders)
        {
            selectedObjectsL.Add(x.gameObject);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beginGameTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.A)){
            GameManager.Instance.SetComponents(true);
            this.gameObject.SetActive(false);
        }
    }
}

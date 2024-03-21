using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OpenFurnace : MonoBehaviour
{
    public GameObject obj;

    public void Click() {
       var angle =  obj.transform.localEulerAngles;
        if (angle.z == 270.0) {
            obj.transform.rotation = Quaternion.Euler(90, 0, 0);

        }
        else {
            obj.transform.rotation = Quaternion.Euler(90, 90, 0);
        }
       

    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitVr : MonoBehaviour
{
    void Update() {
        if (Input.GetKey(KeyCode.Escape)){
            Application.Quit(); 
        }
    }
    public void Exit() {

        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

    }
}


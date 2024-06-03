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
        if (Input.GetKey("escape"))  // если нажата клавиша Esc (Escape)
      {
            Application.Quit();    // закрыть приложение
        }
    }
    public void Exit() {

       // if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        //}

    }
}


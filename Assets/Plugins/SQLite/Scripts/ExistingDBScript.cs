using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Entity.Models;


public class ExistingDBScript : MonoBehaviour {

    public Text DebugText;

    void Start() {
        var ds = new DataService("mainV2.db");
        var task = ds.GetTask();
        foreach(var ts in task) {
            DebugText.text += ts;
        }
    }
}

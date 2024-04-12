using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Entity.Models;

public class ExistingDBScript : MonoBehaviour {

    public Text DebugText;

    // Use this for initialization
    void Start() {
        var ds = new DataService("existing.db");
        ds.GetTask();
        ds.GetMMs();

       // var people = ds.GetUser();
       // ToConsole(people);

        //people = ds.GetUserWhere();
        //ToConsole("Searching for Roberto ...");
       // ToConsole(people);

        var task = ds.GetTask();
       // ToConsole(task);

        task = ds.GetTaskWhere();
        ToConsole(task);

        //ds.CreatePerson();
        //ToConsole("New person has been created");
        //var p = ds.GetJohnny();
        //ToConsole(p.ToString());

        //var p = ds.GetScript();
        //var p1 = ds.GetScripts();
        //  ToConsole(p);
        //ToConsole(p.ToString());
        //ToConsole(p1.ToString());
    }

    private void ToConsole(IEnumerable<Tasks> people) {
        foreach (var person in people) {
            ToConsole(person.ToString());
        }
    }

    private void ToConsole(string msg) {
        DebugText.text += System.Environment.NewLine + msg;
        Debug.Log(msg);
    }

}

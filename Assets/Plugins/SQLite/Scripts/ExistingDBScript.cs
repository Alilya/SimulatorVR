using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Entity.Models;


public class ExistingDBScript : MonoBehaviour {

    public Text DebugText;

    // Use this for initialization
    void Start() {
        //Scripts scripts = new Scripts();

        //scripts.Id = 3;
        //scripts.Status = "Tom";
        //scripts.Protocol = "Perez";
        //scripts.TaskId = 1;
        //scripts.InstructorId = 1;
        //scripts.TraineeId = 1;

        var ds = new DataService("mainV2.db");

        //ds.GetTask();
        // ds.GetMMs();
        //ds.InsertScript(scripts);
        // var scrip = ds.GetScript();
        // ToConsole(scrip);

        // var people = ds.GetUser();
        // ToConsole(people);

        //people = ds.GetUserWhere();
        //ToConsole("Searching for Roberto ...");
        // ToConsole(people);

        var task = ds.GetTask();
        foreach(var ts in task) {
            DebugText.text += ts;
        }
        
        //ToConsole(task);

        // task = ds.GetTaskWhere();
        //ToConsole(task);

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

using SQLite4Unity3d;
using UnityEngine;
using Entity.Models;
using System.Linq;
using UnityEngine.Windows;



#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService {

    private SQLiteConnection _connection;

    public DataService(string DatabaseName) {

#if UNITY_EDITOR
        var dbPath = string.Format(@"C:\Users\Alina\Desktop\СПБГТИ(ТУ)\Diplom\СПЕКАНИЕ\Проект Шишко Колесникова\Sintering-of-ceramics\Sintering of ceramics\bin\Debug\net6.0-windows\{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);

    }

    public void InsertScriptDB() {

        _connection.Insert(new[]{
            new Scripts{
                Id = 1,
                Status = "Tom",
                Protocol = "Perez",
                TaskId = 56,
                InstructorId= 1,
               // TraineeId= 1,
            },
        });


    }

    public IEnumerable<Users> GetUser() {
        return _connection.Table<Users>();
    }

    public IEnumerable<Users> GetUserWhere() {
        return _connection.Table<Users>().Where(x => x.Id == 1);
    }

    //public IEnumerable<Tasks> GetTask() {
    //    //Tasks tasks = new Tasks();
    //    // Qualities qualities = new Qualities();
    //    var qu = _connection.Table<Qualities>();
    //    var ts = _connection.Table<Tasks>();
    //    var oven = _connection.Table<Equipments>();
    //    var mater = _connection.Table<Materials>();

    //    if (ts.First().Id == qu.First().Id) {
    //        Debug.Log(qu.First().Alias + " название");
    //    }
    //    if (ts.First().Id == oven.First().Id) {
    //        Debug.Log(oven.First().Manufacturer + " тип печи");
    //    }
    //    if (ts.First().Id == mater.First().Id) {
    //        Debug.Log(mater.First().Name + " материал");
    //    }
    //    Debug.Log(ts.First().Reference + " эталон");

    //    return _connection.Table<Tasks>();
    //}
    public List<string> GetTask() {
        var qu = _connection.Table<Qualities>();
        var ts = _connection.Table<Tasks>();
        var oven = _connection.Table<Equipments>();
        var mater = _connection.Table<Materials>();
        List<string> list = new List<string>();
        if (ts.First().Id == qu.First().Id) {
            list.Add("Показатель качества: " + qu.First().Alias + System.Environment.NewLine);
        }
        if (ts.First().Id == oven.First().Id) {
            list.Add("Тип печи: " + oven.First().Manufacturer + System.Environment.NewLine);
        }
        if (ts.First().Id == mater.First().Id) {
            list.Add("Материал: " + mater.First().Name + System.Environment.NewLine);
        }
        list.Add("Требуемое значение: " + ts.First().Id + System.Environment.NewLine);

        return list;
    }
    public int InsertScript(Scripts scripts) {
        return _connection.Insert(scripts);
    }

    public IEnumerable<Tasks> GetTaskWhere() {
        return _connection.Table<Tasks>().Where(x => x.MaterialId == 2);
    }

    public MMs GetMMs() {
        return _connection.Table<MMs>().Where(x => x.Id == 1).FirstOrDefault();
    }

    public IEnumerable<Scripts> GetScript() {
        return _connection.Table<Scripts>();
    }

    public IEnumerable<Scripts> GetScripts() {
        return _connection.Table<Scripts>().Where(x => x.Id == 1);
    }

    public Person CreatePerson() {
        var p = new Person {
            Name = "Johnny",
            Surname = "Mnemonic",
            Age = 21
        };
        _connection.Insert(p);
        return p;
    }
}

using Mathematics;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using Entity;
using System.Linq;
using System.IO;
using SQLite4Unity3d;

public class VrButton : MonoBehaviour {
    public bool isChangeColor = false;
    private Color _color_start;
    private Image _button;
    [Serializable] public class ButtonEvent : UnityEvent { }

    public ButtonEvent down;
    public ButtonEvent press;
    public ButtonEvent up;

    public TMP_Text tempValStart;
    public TMP_Text tempValEnd;
    public TMP_Text timeVal;
    public TMP_Text presVal;

    public InputField tempValF;
    public InputField timeValF;
    public InputField presValF;
    public Text textResult;

    public void Start() {
        //Context context = new Context("mainV1.db");
        //var materials = context.Materials.ToList();
        //Console.WriteLine(materials);
        //string path = "C:\\Users\\Alina\\OneDrive\\Рабочий стол\\СПБГТИ(ТУ)\\Diplom\\СПЕКАНИЕ\\SinteringSimulatorDiplomProject\\Assets\\StreamingAssets\\mainV1.db";
        //SQLiteConnection conn = new SQLiteConnection(path);
        //полная перезапись файла
        //string path = "logsRes.txt";
       // StreamWriter writer = new StreamWriter(path, true);

       // writer.WriteLineAsync("Addition");
        //writer.WriteAsync(text + " " + time);
    }

    public void ClickButtonCalc() {
        Sintering model = new Sintering(
                   t0: 20,//Convert.ToDouble(tempValStart.text),//20
                   tk: 40,//Convert.ToDouble(tempValEnd.text),//temp
                   l0: 1 * 0.000001,
                   p0: 40,
                   tau1: Convert.ToDouble(timeVal.text),//70*60
                   d: 0.1 * 0.000000001,
                   db0: 0.35,
                   ds0: 0.4,
                   eb: 171.5 * 1000,
                   es: 245 * 1000,
                   s: 3.5,
                   eta0: 170 * 1000000,
                   pg: 40,//Convert.ToDouble(presVal.text),//press * 1000000
                   m: 0.1,
                   ro0: 14600,
                   tau2: 60 * 60);

        var result = model.Calculate(true);
        //string txt= "Конечный диаметр зерна,мкм = " + result.LL + '\n' +
        // "Конечная пористость,% = " + result.PP + '\n' +
        // "Конечная плотность,кг/м^3 = " + result.Ro + "   " + tempValStart.text + "   " + tempValEnd.text + "   " + timeVal.text + "   " + presVal.text;

        string txt = result.LL + '\n' + result.PP + '\n' + result.Ro +
            '\n' + tempValStart.text + '\n'
            + tempValEnd.text + '\n' + timeVal.text + '\n' + presVal.text;

        printTxt(txt, DateTime.Now);
       
        textResult.text = "Конечный диаметр зерна,мкм = " + result.LL + '\n' +
         "Конечная пористость,% = " + result.PP + '\n' +
         "Конечная плотность,кг/м^3 = " + result.Ro+ "   "+tempValStart.text+ "   " + tempValEnd.text+ "   " + timeVal.text+ "   " + presVal.text;



        // var context = new Context("mainV1.db");

    }

    
    public async void printTxt(string text, DateTime time) {
        string path = "logsRes.txt";
        Debug.Log(text);
        // полная перезапись файла 
        //StreamWriter writer = new StreamWriter(path, true);

        //writer.WriteLineAsync("Addition");
        //writer.WriteAsync(text+ " "+time);

        using (StreamWriter writer = new StreamWriter(path, true)) {
            await writer.WriteAsync(text + " " + time);
        }
    }

    private void OnEnable() {
        TryGetComponent(out _button);
        if (_button && isChangeColor)
            _color_start = _button.color;
    }

    private void OnDisable() {
        if (_button && isChangeColor)
            _button.color = _color_start;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "hand") {
            down?.Invoke();
            if (_button && isChangeColor)
                _button.color = Color.gray;
            StartCoroutine(Stay());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "hand") {
            up?.Invoke();
            if (_button && isChangeColor)
                _button.color = _color_start;
            StopCoroutine(Stay());
        }
    }

    private IEnumerator Stay() {
        while (true) {
            press?.Invoke();
            yield return new WaitForSeconds(0.1f);
        }
    }
}

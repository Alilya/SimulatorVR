using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Mathematics;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

public class EntryValuePanel : MonoBehaviour {

    public delegate void ChangeVal(TMP_Text message);
    public event ChangeVal OnChangeVal;

    Regex inReg = new Regex("^-?(\\d){0,3}(,(\\d){0,2})?$");

    [SerializeField] VrButton[] valBtns = new VrButton[10];
    [SerializeField] VrButton minusBtn;
    [SerializeField] VrButton commaBtn;
    [SerializeField] VrButton delBtn;
    [SerializeField] VrButton entrBtn;

    [SerializeField] TMP_Text valText;

    TMP_Text targetField;
    [SerializeField] TMP_Text startTemp;
    [SerializeField] TMP_Text endTemp;
    [SerializeField] TMP_Text time;
    [SerializeField] TMP_Text press;

    VrButton btn = new VrButton();
    public UnityEngine.UI.Text textResult;

    void Start() {
        ClickButtonCalc();
        DataService ds = new DataService("mainV2.db") { };
        List<string> task = ds.GetTask();
        for(int i=0; i< task.Count; i++) {
            printTxt(task[i]);
        }
        
        for (int i = 0; i < valBtns.Length; ++i) {
            int elem = i;
            valBtns[i].down.AddListener(() => {
                string resStr = valText.text + elem.ToString();
                if (inReg.IsMatch(resStr)) {
                    valText.text = resStr;
                }
            });
        }

        minusBtn.down.AddListener(() => {
            if (valText.text.Length == 0) {
                valText.text = "-";
            }
        });

        commaBtn.down.AddListener(() => {
            string resStr = valText.text + ",";
            if (inReg.IsMatch(resStr)) {
                valText.text = resStr;
            }
        });

        delBtn.down.AddListener(() => {
            if (valText.text.Length == 0)
                return;
            valText.text = valText.text.Substring(0, valText.text.Length - 1);
        });

        entrBtn.down.AddListener(() => {
            if (valText.text.Length == 0 ||
                (valText.text.Length == 1 && valText.text == "-")) {
                targetField.text = "0";
            }
            else {
                targetField.text = string.Format("{0:f}", double.Parse(valText.text));

            }
            ClickButtonCalc();
            CalculateImpericalModels(startTemp, time, press);
            switch (targetField.tag) {
                case "startTemp":
                    printTxt("==��������� �����������, 0�  " + DateTime.Now +"   "+ valText.text  + Environment.NewLine);
                    break;
                case "endTemp":
                    printTxt("==�������� �����������, 0�  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                    break;
                case "time":
                    printTxt("==����� ��������, ���  " + DateTime.Now + "   " + valText.text  + Environment.NewLine);
                    break;
                case "press":
                    printTxt("==�������� ����, ���  " + DateTime.Now + "   " + valText.text  + Environment.NewLine);
                    break;
            }
          
            OnChangeVal?.Invoke(targetField);
            gameObject.SetActive(false);
        });

       
        this.gameObject.SetActive(false);
      
      

    }
    public void ClickButtonCalc() {
        Sintering model = new Sintering(
                   t0: Convert.ToDouble(startTemp.text),//20
                   tk: Convert.ToDouble(endTemp.text),//temp
                   l0: 1 * 0.000001,
                   p0: 40,
                   tau1: Convert.ToDouble(time.text),//70*60
                   d: 0.1 * 0.000000001,
                   db0: 0.35,
                   ds0: 0.4,
                   eb: 171.5 * 1000,
                   es: 245 * 1000,
                   s: 3.5,
                   eta0: 170 * 1000000,
                   pg: Convert.ToDouble(press.text),//press * 1000000
                   m: 0.1,
                   ro0: 14600,
                   tau2: 60 * 60);

        var result = model.Calculate(true);
        string txt = "--�������� ������� �����,���  " + DateTime.Now + "  " + result.LL + Environment.NewLine +
         "--�������� ����������,%  " + DateTime.Now + "  " + result.PP + Environment.NewLine +
         "--�������� ���������,��/�^3  " + DateTime.Now + "  " + result.Ro  + Environment.NewLine
        +"��������� �����������,0�  " + DateTime.Now + "  " + startTemp.text + Environment.NewLine
        + "�������� �����������,0�  " + DateTime.Now + "  " + endTemp.text + Environment.NewLine
        + "����� ��������,���  " + DateTime.Now + "  " + time.text + Environment.NewLine
        + "��������,���  " + DateTime.Now + "  " + press.text ;

        printTxt(txt + Environment.NewLine);

        textResult.text = "�������� ������� �����,��� = " + Math.Round(result.LL, 5) + '\n' +
         "�������� ����������,% = " + Math.Round(result.PP, 5) + '\n' +
         "�������� ���������,��/�^3 = " + Math.Round(result.Ro, 5);

    }
    private void CalculateImpericalModels(TMP_Text startTemp, TMP_Text time, TMP_Text press) {
        //string taskPath = System.IO.Directory.GetCurrentDirectory() + "/script.txt";
        string taskPath = "C:\\Users\\Alina\\Desktop\\������(��)\\Diplom\\��������\\������ ����� �����������\\Sintering-of-ceramics\\Sintering of ceramics\\bin\\Debug\\net6.0-windows\\script.txt";

        var lines = System.IO.File.ReadAllLines(taskPath);
        var empiricModels = new List<List<string>>();

        foreach (var line in lines) {
            if (line.Contains("//")) {
                var split = line.Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                empiricModels.Add(split.ToList());
            } 
        }
        empiricModels.ToArray();
        var expression = "";
        var result = "";
        for (int i=0; i<empiricModels.Count;i++) {
            result = empiricModels[i][0];
            while (empiricModels[i][0].Contains("Pg")) {
                expression=empiricModels[i][0].Replace("Pg", press.text.ToString());
                
            }
            while (empiricModels[i][0].Contains("T")) {
                expression=empiricModels[i][0].Replace("T", startTemp.text.ToString());
            }
            while (empiricModels[i][0].Contains("tao")) {
                expression=empiricModels[i][0].Replace("tao", time.text.ToString());
            }
            result = expression;
            expression = empiricModels[i][0].ToString();
           // result = Parser.Parse(expression);
            Debug.Log(expression + "  RESULT MODEL CALC");
        }
        //result=Parser.Parse(expression);
        //Debug.Log(result+"  RESULT MODEL CALC");
    }

   
    public async void printTxt(string text) {
         //string logPath = "C:/Users/Alina/Desktop/������(��)/Diplom/��������/������ ����� �����������/Sintering-of-ceramics/Sintering of ceramics/bin/Debug/net6.0-windows/logsRes.txt";
        string logPath = System.IO.Directory.GetCurrentDirectory()+ "/logsRes.txt";
        Debug.Log(text);
        // ������ ���������� ����� 
        //StreamWriter writer = new StreamWriter(path, true);

        //writer.WriteLineAsync("Addition");
        //writer.WriteAsync(text+ " "+time);

        using (StreamWriter writer = new StreamWriter(logPath, true)) {
            await writer.WriteAsync(text + Environment.NewLine);
        }
      
    }
    public void Open(TMP_Text t) {
        targetField = t;
        valText.text = t.text;
        var s = t.tag;
        gameObject.SetActive(true);
        //Starter();
    }
}

using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using Mathematics;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Diagnostics;
using static Parser;
using System.Globalization;
using UnityEngine.Windows;
using Unity.VisualScripting;



public class EntryValuePanel : MonoBehaviour {

    public delegate void ChangeVal(TMP_Text message);
    public event ChangeVal OnChangeVal;

    Regex inReg = new Regex("^-?(\\d){0,4}(,(\\d){0,2})?$");

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
        printTxt("Начальная температура,°C  " + DateTime.Now + "  " + startTemp.text + Environment.NewLine
      + "Конечная температура,°C  " + DateTime.Now + "  " + endTemp.text + Environment.NewLine
      + "Время спекания,мин  " + DateTime.Now + "  " + time.text + Environment.NewLine
      + "Давление,атм  " + DateTime.Now + "  " + press.text);

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
            string taskPath = System.IO.Directory.GetCurrentDirectory() + "/script.txt";
           //string taskPath = "C:\\Users\\Alina\\Desktop\\СПБГТИ(ТУ)\\Diplom\\СПЕКАНИЕ\\Проект Шишко Колесникова\\Sintering-of-ceramics\\Sintering of ceramics\\bin\\Debug\\net6.0-windows\\script.txt";
            var lines = System.IO.File.ReadAllLines(taskPath);

            int count = 0;
            foreach (var line in lines) {
                if (line.Contains("//")) {
                    count++;
                }
            }
            if (count != 0) {
                CalculateImpericalModels(endTemp, time, press);
            }
            else {
                ClickButtonCalc();
            }

            switch (targetField.tag) {
                case "startTemp":
                    printTxt("==Начальная температура, °C  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                    break;
                case "endTemp":
                    printTxt("==Конечная температура, °C  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                    break;
                case "time":
                    printTxt("==Время спекания, мин  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                    break;
                case "press":
                    printTxt("==Давление газа, атм  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                    break;
            }

            OnChangeVal?.Invoke(targetField);
            gameObject.SetActive(false);
        });
        this.gameObject.SetActive(false);
    }

    public void ClickButtonCalc() {
        string taskPath = System.IO.Directory.GetCurrentDirectory() + "/script.txt";
       // string taskPath = "C:\\Users\\Alina\\Desktop\\СПБГТИ(ТУ)\\Diplom\\СПЕКАНИЕ\\Проект Шишко Колесникова\\Sintering-of-ceramics\\Sintering of ceramics\\bin\\Debug\\net6.0-windows\\script.txt";
        var lines = System.IO.File.ReadAllLines(taskPath);
        var arrCoeff = new List<List<string>>();
        foreach (var line in lines) {
            if (line.Contains("*")) {
                var split = line.Split(new[] { "*", " " }, StringSplitOptions.RemoveEmptyEntries);
                arrCoeff.Add(split.ToList());
            }
        }
        arrCoeff.ToArray();
        Sintering model = new Sintering(
                   t0: Convert.ToDouble(startTemp.text),
                   tk: Convert.ToDouble(endTemp.text),
                   l0: Convert.ToDouble(arrCoeff[0][0]) / 1000000.0,//1
                   p0: Convert.ToDouble(arrCoeff[0][1]),//40
                   tau1: Convert.ToDouble(time.text)*60,
                   d: Convert.ToDouble(arrCoeff[0][2]) * 0.000000001,//0,1
                   db0: Convert.ToDouble(arrCoeff[0][3]),//0,35
                   ds0: Convert.ToDouble(arrCoeff[0][4]),//0,4
                   eb: Convert.ToDouble(arrCoeff[0][5]) * 1000,//181
                   es: Convert.ToDouble(arrCoeff[0][6]) * 1000,//245
                   s: Convert.ToDouble(arrCoeff[0][7]),//3,5
                   eta0: Convert.ToDouble(arrCoeff[0][8]) * 1000000,//170
                   pg: Convert.ToDouble(press.text) * 101325.0,
                   m: Convert.ToDouble(arrCoeff[0][9]),//0,1
                   ro0: Convert.ToDouble(arrCoeff[0][10]),//14600
                   tau2: 30 * 60);

        var result = model.Calculate(true,1,1,10);

        string txt = "--Конечный диаметр зерна,мкм  " + DateTime.Now + "  " + result.LL + Environment.NewLine +
         "--Конечная пористость,%  " + DateTime.Now + "  " + result.PP + Environment.NewLine +
         "--Конечная плотность,кг/см³  " + DateTime.Now + "  " + result.Ro + Environment.NewLine +
         "!Начальная температура,°C  " + DateTime.Now + "  " + startTemp.text + Environment.NewLine
      + "!Конечная температура,°C  " + DateTime.Now + "  " + endTemp.text + Environment.NewLine
      + "!Время спекания,мин  " + DateTime.Now + "  " + time.text + Environment.NewLine
      + "!Давление,атм  " + DateTime.Now + "  " + press.text + Environment.NewLine;

        printTxt(txt + Environment.NewLine);

        textResult.text = "Конечный диаметр зерна,мкм = " + Math.Round(result.LL, 2) + '\n' +
         "Конечная пористость,% = " + Math.Round(result.PP, 2) + '\n' +
         "Конечная плотность,кг/см³ = " + Math.Round(result.Ro, 2);

    }

    private void CalculateImpericalModels(TMP_Text endTemp, TMP_Text time, TMP_Text press) {
            
        double pr = Convert.ToDouble(press.text);
        double t = Convert.ToDouble(endTemp.text);
        double ti = Convert.ToDouble(time.text);

        double res1 = -17.46 - 0.00622 * pr + 0.04293 * t + 0.000015 * pr * t - 0.000014 * t * t - 0.000000005 * pr * t * t;
        double res2 = -58231 + 673.2 * pr + 81.76 * t - 0.9453 * pr * t - 13.1 * pr * pr - 0.02666 * t * t + 0.0184 * pr * pr * t + 0.00031 * pr * t * t - 0.000006 * pr * pr * t * t;
        double res3 = 199.4 - 0.2765 * t - 4.486 * ti + 0.0062 * t * ti + 0.000096 * t * t + 0.0449 * ti * ti - 0.000002 * t * t * ti - 0.000062 * t * ti * ti + 0.00000002 * t * t * ti * ti;
        double res4 = 14.64 + 0.01683 * ti + 0.1033 * t + 0.00012 * t * ti - 0.00014 * ti * ti - 0.000034 * t * t - 0.00000097 * ti * ti * t - 0.00000004 * ti * t * t + 0.0000000003 * t * t * ti * ti;

        string txt = "-_-Плотность твердого сплава, кг/см³  " + DateTime.Now + "  " + Math.Round(res1 * 1000,0) + Environment.NewLine +
       "-_-Прочность твердого сплава при поперечном изгибе,МПа  " + DateTime.Now + "  " + Math.Round(res2, 0) + Environment.NewLine +
       "-_-Остаточная пористость,%  " + DateTime.Now + "  " + Math.Round(res3, 1) + Environment.NewLine +
       "-_-Твердость сплава,ед  " + DateTime.Now + "  " + Math.Round(res4, 1) + Environment.NewLine;

        printTxt(txt + Environment.NewLine);

        textResult.text = "Плотность твердого сплава, кг/см³  " + Math.Round(res1*1000, 0) + Environment.NewLine +
       "Прочность твердого сплава при поперечном изгибе,МПа  " + Math.Round(res2, 0) + Environment.NewLine +
       "Остаточная пористость,%  " + Math.Round(res3, 1) + Environment.NewLine +
       "Твердость сплава,ед  " + Math.Round(res4, 1) + Environment.NewLine;


    }
   
    public async void printTxt(string text) {
        //string logPath = "C:/Users/Alina/Desktop/СПБГТИ(ТУ)/Diplom/СПЕКАНИЕ/Проект Шишко Колесникова/Sintering-of-ceramics/Sintering of ceramics/bin/Debug/net6.0-windows/logsRes.txt";
        string logPath = System.IO.Directory.GetCurrentDirectory()+ "/logsRes.txt";
        using (StreamWriter writer = new StreamWriter(logPath, true)) {
            await writer.WriteAsync(text + Environment.NewLine);
        }
    }
    public void Open(TMP_Text t) {
        if (valText.text.Length != 0)
            valText.text = "";
        targetField = t;
        valText.text = t.text;
        var s = t.tag;
        gameObject.SetActive(true);
    }
}

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

public class EntryValuePanel : MonoBehaviour {

    public delegate void ChangeVal(TMP_Text message);
    public event ChangeVal OnChangeVal;

    Regex inReg = new Regex("^-?(\\d){0,3}(,(\\d){0,2})?$");

    // [SerializeField] VrButton[] valBtns = new VrButton[10];
    //[SerializeField] VrButton minusBtn;
    // [SerializeField] VrButton commaBtn;
    // [SerializeField] VrButton delBtn;
    // [SerializeField] VrButton entrBtn;

    [SerializeField] TMP_Text valText;

    TMP_Text targetField;
    [SerializeField] TMP_Text startTemp;
    [SerializeField] TMP_Text endTemp;
    [SerializeField] TMP_Text time;
    [SerializeField] TMP_Text press;

    // VrButton btn = new VrButton();
    public UnityEngine.UI.Text textResult;

    //void Start() {
    //    printTxt("Начальная температура,0С  " + DateTime.Now + "  " + startTemp.text + Environment.NewLine
    //  + "Конечная температура,0С  " + DateTime.Now + "  " + endTemp.text + Environment.NewLine
    //  + "Время спекания,мин  " + DateTime.Now + "  " + time.text + Environment.NewLine
    //  + "Давление,атм  " + DateTime.Now + "  " + press.text);

    //    for (int i = 0; i < valBtns.Length; ++i) {
    //        int elem = i;
    //        valBtns[i].down.AddListener(() => {
    //            string resStr = valText.text + elem.ToString();
    //            if (inReg.IsMatch(resStr)) {
    //                valText.text = resStr;
    //            }
    //        });
    //    }
    //    minusBtn.down.AddListener(() => {
    //        if (valText.text.Length == 0) {
    //            valText.text = "-";
    //        }
    //    });

    //    commaBtn.down.AddListener(() => {
    //        string resStr = valText.text + ",";
    //        if (inReg.IsMatch(resStr)) {
    //            valText.text = resStr;
    //        }
    //    });

    //    delBtn.down.AddListener(() => {
    //        if (valText.text.Length == 0)
    //            return;
    //        valText.text = valText.text.Substring(0, valText.text.Length - 1);
    //    });

    //    entrBtn.down.AddListener(() => {
    //        if (valText.text.Length == 0 ||
    //            (valText.text.Length == 1 && valText.text == "-")) {
    //            targetField.text = "0";
    //        }
    //        else {
    //            targetField.text = string.Format("{0:f}", double.Parse(valText.text));

    //        }
    //        //string taskPath = System.IO.Directory.GetCurrentDirectory() + "/script.txt";
    //        string taskPath = "C:\\Users\\Alina\\Desktop\\СПБГТИ(ТУ)\\Diplom\\СПЕКАНИЕ\\Проект Шишко Колесникова\\Sintering-of-ceramics\\Sintering of ceramics\\bin\\Debug\\net6.0-windows\\script.txt";
    //        var lines = System.IO.File.ReadAllLines(taskPath);

    //        int count = 0;
    //        foreach (var line in lines) {
    //            if (line.Contains("//")) {
    //                count++;
    //            }
    //        }
    //        if(count != 0) {
    //            CalculateImpericalModels(startTemp, time, press);
    //        }
    //        else {
    //            ClickButtonCalc();
    //        }

    //        switch (targetField.tag) {
    //            case "startTemp":
    //                printTxt("==Начальная температура, 0С  " + DateTime.Now +"   "+ valText.text  + Environment.NewLine);
    //                break;
    //            case "endTemp":
    //                printTxt("==Конечная температура, 0С  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
    //                break;
    //            case "time":
    //                printTxt("==Время спекания, мин  " + DateTime.Now + "   " + valText.text  + Environment.NewLine);
    //                break;
    //            case "press":
    //                printTxt("==Давление газа, атм  " + DateTime.Now + "   " + valText.text  + Environment.NewLine);
    //                break;
    //        }

    //        OnChangeVal?.Invoke(targetField);
    //        gameObject.SetActive(false);
    //    });
    //    this.gameObject.SetActive(false);
    //}

    public void CalcOpenEntry() {
        printTxt("Начальная температура,0С  " + DateTime.Now + "  " + startTemp.text + Environment.NewLine
      + "Конечная температура,0С  " + DateTime.Now + "  " + endTemp.text + Environment.NewLine
      + "Время спекания,мин  " + DateTime.Now + "  " + time.text + Environment.NewLine
      + "Давление,атм  " + DateTime.Now + "  " + press.text);

        if (valText.text.Length == 0 ||
            (valText.text.Length == 1 && valText.text == "-")) {
            targetField.text = "0";
        }
        else {
            targetField.text = valText.text;

        }
        //string taskPath = System.IO.Directory.GetCurrentDirectory() + "/script.txt";
        string taskPath = "C:\\Users\\Alina\\Desktop\\СПБГТИ(ТУ)\\Diplom\\СПЕКАНИЕ\\Проект Шишко Колесникова\\Sintering-of-ceramics\\Sintering of ceramics\\bin\\Debug\\net6.0-windows\\script.txt";
        var lines = System.IO.File.ReadAllLines(taskPath);

        int count = 0;
        foreach (var line in lines) {
            if (line.Contains("//")) {
                count++;
            }
        }

        if (count != 0) {
            CalculateImpericalModels(startTemp, time, press);
        }
        else {
            ClickButtonCalc();
        }

        switch (targetField.tag) {
            case "startTemp":
                printTxt("==Начальная температура, 0С  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                break;
            case "endTemp":
                printTxt("==Конечная температура, 0С  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                break;
            case "time":
                printTxt("==Время спекания, мин  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                break;
            case "press":
                printTxt("==Давление газа, атм  " + DateTime.Now + "   " + valText.text + Environment.NewLine);
                break;
        }
        this.gameObject.SetActive(false);
    }
    public void ClickButtonCalc() {
        Sintering model = new Sintering(
                   t0: Convert.ToDouble(startTemp.text),
                   tk: Convert.ToDouble(endTemp.text),
                   l0: 1 * 0.000001,
                   p0: 40,
                   tau1: Convert.ToDouble(time.text),
                   d: 0.1 * 0.000000001,
                   db0: 0.35,
                   ds0: 0.4,
                   eb: 171.5 * 1000,
                   es: 245 * 1000,
                   s: 3.5,
                   eta0: 170 * 1000000,
                   pg: Convert.ToDouble(press.text),
                   m: 0.1,
                   ro0: 14600,
                   tau2: 60 * 60);

        var result = model.Calculate(true);
        string txt = "--Конечный диаметр зерна,мкм  " + DateTime.Now + "  " + result.LL + Environment.NewLine +
         "--Конечная пористость,%  " + DateTime.Now + "  " + result.PP + Environment.NewLine +
         "--Конечная плотность,кг/м^3  " + DateTime.Now + "  " + result.Ro + Environment.NewLine +
         "!Начальная температура,0С  " + DateTime.Now + "  " + startTemp.text + Environment.NewLine
      + "!Конечная температура,0С  " + DateTime.Now + "  " + endTemp.text + Environment.NewLine
      + "!Время спекания,мин  " + DateTime.Now + "  " + time.text + Environment.NewLine
      + "!Давление,атм  " + DateTime.Now + "  " + press.text + Environment.NewLine;

        printTxt(txt + Environment.NewLine);

        textResult.text = "Конечный диаметр зерна,мкм = " + Math.Round(result.LL, 5) + '\n' +
         "Конечная пористость,% = " + Math.Round(result.PP, 5) + '\n' +
         "Конечная плотность,кг/м^3 = " + Math.Round(result.Ro, 5);

    }
    private void CalculateImpericalModels(TMP_Text startTemp, TMP_Text time, TMP_Text press) {
        //string taskPath = System.IO.Directory.GetCurrentDirectory() + "/script.txt";
        string taskPath = "C:\\Users\\Alina\\Desktop\\СПБГТИ(ТУ)\\Diplom\\СПЕКАНИЕ\\Проект Шишко Колесникова\\Sintering-of-ceramics\\Sintering of ceramics\\bin\\Debug\\net6.0-windows\\script.txt";

        var lines = System.IO.File.ReadAllLines(taskPath);
        var empiricModels = new List<List<string>>();

        foreach (var line in lines) {
            if (line.Contains("//")) {
                var split = line.Split(new[] { "//" }, StringSplitOptions.RemoveEmptyEntries);
                empiricModels.Add(split.ToList());
            }
        }
        empiricModels.ToArray();
        string expression = "";
        double result;
        List<double> results = new List<double>();
        for (int i = 0; i < empiricModels.Count; i++) {
            expression = empiricModels[i][0].ToString();
            if (expression.Contains("Pg") || expression.Contains("T") || expression.Contains("tao")) {
                expression = expression.Replace("Pg", press.text.ToString());
                //expression = expression.Replace("Pg", "40");
                expression = expression.Replace("T", startTemp.text.ToString());
                //expression = expression.Replace("T", "1300");
                expression = expression.Replace("tao", time.text.ToString());
                //expression = expression.Replace("tao", "60");
                expression = expression.Replace(",", ".");
                expression = expression.Replace("+-", "-");

                result = Evaluate(expression);
                results.Add(result);
            }

        }
        string txt = "-_-Плотность твердого сплава,г/см3  " + DateTime.Now + "  " + results[0] + Environment.NewLine +
       "-_-Прочность твердого сплава при поперечном изгибе,МПа  " + DateTime.Now + "  " + results[1] + Environment.NewLine +
       "-_-Остаточная пористость твердого сплава,%  " + DateTime.Now + "  " + results[2] + Environment.NewLine +
       "-_-Твердость сплава,ед  " + DateTime.Now + "  " + results[3] + Environment.NewLine;
        ;

        printTxt(txt + Environment.NewLine);

        textResult.text = "Плотность твердого сплава,г/см3  " + results[0] + Environment.NewLine +
       "Прочность твердого сплава при поперечном изгибе,МПа  " + results[1] + Environment.NewLine +
       "Остаточная пористость твердого сплава,%  " + results[2] + Environment.NewLine +
       "Твердость сплава,ед  " + results[3] + Environment.NewLine;
    }
    static double Evaluate(string expression) {
        var loDataTable = new DataTable();
        var loDataColumn = new DataColumn("Eval", typeof(double), expression);
        loDataTable.Columns.Add(loDataColumn);
        loDataTable.Rows.Add(0);
        return (double)(loDataTable.Rows[0]["Eval"]);
    }

    public async void printTxt(string text) {
        string logPath = "C:/Users/Alina/Desktop/СПБГТИ(ТУ)/Diplom/СПЕКАНИЕ/Проект Шишко Колесникова/Sintering-of-ceramics/Sintering of ceramics/bin/Debug/net6.0-windows/logsRes.txt";
        //string logPath = System.IO.Directory.GetCurrentDirectory()+ "/logsRes.txt";
        // полная перезапись файла 
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
    }
}

using Mathematics;
using System;
using UnityEngine;
using UnityEngine.UI;
//using Unity.NLog;
using Unity;
using UnityEngine.Assertions;
using System.IO;



public class ButtonClick : MonoBehaviour {
    private double temp = 1200;
    private double time = 0;
    private double press = 40;

    public Text tempVal;
    public InputField tempValF;
    public Text timeVal;
    public InputField timeValF;
    public Text presVal;
    public InputField presValF;

    public Text textResult;

    public void Start() {
        Sintering model = new Sintering(
                  t0: 20,
                  tk: temp,
                  l0: 1 * 0.000001,
                  p0: 40,
                  tau1: 70 * 60,
                  d: 0.1 * 0.000000001,
                  db0: 0.35,
                  ds0: 0.4,
                  eb: 171.5 * 1000,
                  es: 245 * 1000,
                  s: 3.5,
                  eta0: 170 * 1000000,
                  pg: press * 1000000,
                  m: 0.1,
                  ro0: 14600,
                  tau2: time * 60);


        var result = model.Calculate(true);
        Debug.Log( "Конечный диаметр зерна,мкм = " + result.LL + '\n' +
         "Конечная пористость,% = " + result.PP + '\n' +
         "Конечная плотность,кг/м^3 = " + result.Ro);

    }
    public void ClickButtonCalc() {
        Sintering model = new Sintering(
                   t0: 20,
                   tk: temp,
                   l0: 1 * 0.000001,
                   p0: 40,
                   tau1: 70 * 60,
                   d: 0.1 * 0.000000001,
                   db0: 0.35,
                   ds0: 0.4,
                   eb: 171.5 * 1000,
                   es: 245 * 1000,
                   s: 3.5,
                   eta0: 170 * 1000000,
                   pg: press * 1000000,
                   m: 0.1,
                   ro0: 14600,
                   tau2: time * 60);


        var result = model.Calculate(true);
        textResult.text = " Ett = " + result.Ett+'\n'+
         " LL = "+ result.LL + '\n'+
         " PP = " + result.PP + '\n'+
         " PPP = " + result.PPP + '\n'+
         " Ro = " +result.Ro;



        // var context = new Context("mainV1.db");
   
    }
    
    public async void printTxt(string text, DateTime time) {
        string path = "note1.txt";
       // полная перезапись файла 
        //StreamWriter writer = new StreamWriter(path, true);
         
        //writer.WriteLineAsync("Addition");
        //writer.WriteAsync(text+ " "+time);

        using (StreamWriter writer = new StreamWriter(path, true)) { 
            await writer.WriteAsync(text + " " + time);
        }
    }

    public void ClickButtonTempMinus() {
        temp = Convert.ToDouble(tempVal.text);
        temp -= 10;
        tempValF.text = temp.ToString();
       
        printTxt("Температура: Т = " + temp.ToString()+"0С Время ", DateTime.Now);
    }
    public void ClickButtonTempPlus() {
        temp = Convert.ToDouble(tempVal.text);
        temp += 10;
        tempValF.text = temp.ToString();
        printTxt("Температура: Т = " + temp.ToString() + "0С Время ", DateTime.Now);
    
}
    public void ClickButtonTimePlus() {
        time = Convert.ToDouble(timeVal.text);
        time += 10;
        timeValF.text = time.ToString();
        printTxt("Время: time = " + temp.ToString() + "0С Время ", DateTime.Now);
    
}
    public void ClickButtonTimeMin() {
        time = Convert.ToDouble(timeVal.text);
        time -= 10;
        timeValF.text = time.ToString();
        printTxt("Время: time = " + temp.ToString() + "0С Время ", DateTime.Now);
    }
    public void ClickButtonPrPlus() {
        press = Convert.ToDouble(presVal.text);
        press += 10;
        presValF.text = press.ToString();
        printTxt("Давление: press = " + temp.ToString() + "0С Время ", DateTime.Now);
    }
    public void ClickButtonPrMin() {
        press = Convert.ToDouble(presVal.text);
        press -= 10;
        presValF.text = press.ToString();
        printTxt("Давление: press = " + temp.ToString() + "0С Время ", DateTime.Now);
    }

    public void InMethodInitializationTest() {
        //var container = new UnityContainer();
        //container
        //    .AddNewExtension<BuildTracking>()
        //    .AddNewExtension<LogCreation<ILogger, NLogFactory>>();

        //var log = container.Resolve<ILogger>();

        //log.Debug("test debug");

        //Assert.AreEqual("Common.Test.LogTest", log.Name);
    }
}


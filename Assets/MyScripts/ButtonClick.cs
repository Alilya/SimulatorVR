using Mathematics;
using System;
using UnityEngine;
using UnityEngine.UI;
//using Unity.NLog;
using Unity;
using UnityEngine.Assertions;
using System.IO;
using TMPro;



public class ButtonClick : MonoBehaviour {
    private double temp = 1200;
    private double time = 0;
    private double press = 40;

    public TMP_Text tempValStart;
    public TMP_Text tempValEnd;
    public TMP_Text timeVal;
    public TMP_Text presVal;


    public InputField tempValF;
    public InputField timeValF;
    public InputField presValF;
    public Text textResult;

    public void ClickButtonCalc() {
        Sintering model = new Sintering(
                   t0: Convert.ToDouble(tempValStart.text),//20
                   tk: Convert.ToDouble(tempValEnd.text),//temp
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
                   pg: Convert.ToDouble(presVal.text),//press * 1000000
                   m: 0.1,
                   ro0: 14600,
                   tau2: time * 60);


        var result = model.Calculate(true);
        textResult.text = "BC Конечный диаметр зерна,мкм = " + result.LL + '\n' +
         "Конечная пористость,% = " + result.PP + '\n' +
         "Конечная плотность,кг/м^3 = " + result.Ro;

        // var context = new Context("mainV1.db");

    }

    public void ClickButtonTempMinus() {
        temp = Convert.ToDouble(tempValStart.text);
        temp -= 10;
        tempValF.text = temp.ToString();

        
    }
    public void ClickButtonTempPlus() {
        temp = Convert.ToDouble(tempValStart.text);
        temp += 10;
        tempValF.text = temp.ToString();
       

    }
    public void ClickButtonTempEndMinus() {
        temp = Convert.ToDouble(tempValEnd.text);
        temp -= 10;
        tempValF.text = temp.ToString();

       
    }
    public void ClickButtonTempEndPlus() {
        temp = Convert.ToDouble(tempValEnd.text);
        temp += 10;
        tempValF.text = temp.ToString();
        

    }
    public void ClickButtonTimePlus() {
        time = Convert.ToDouble(timeVal.text);
        time += 10;
        timeValF.text = time.ToString();
      

    }
    public void ClickButtonTimeMin() {
        time = Convert.ToDouble(timeVal.text);
        time -= 10;
        timeValF.text = time.ToString();
        
    }
    public void ClickButtonPrPlus() {
        press = Convert.ToDouble(presVal.text);
        press += 10;
        presValF.text = press.ToString();
       
    }
    public void ClickButtonPrMin() {
        press = Convert.ToDouble(presVal.text);
        press -= 10;
        presValF.text = press.ToString();
        
    }
}


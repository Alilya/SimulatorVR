using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using static VrButton;
using UnityEngine.UI;
using Mathematics;
using System;



public class EntryValuePanel : MonoBehaviour
{
    public delegate void ChangeVal(TMP_Text message);
    public event ChangeVal OnChangeVal;

    Regex inReg = new Regex("^-?(\\d){0,3}(,(\\d){0,2})?$");

    [SerializeField] VrButton[] valBtns = new VrButton[10];
    [SerializeField] VrButton minusBtn;
    [SerializeField] VrButton commaBtn;
    [SerializeField] VrButton delBtn;
    [SerializeField] VrButton entrBtn;

    [SerializeField] TMP_Text valText;


    //public ButtonEvent down;
    //public ButtonEvent press;
    //public ButtonEvent up;

    //public TMP_Text tempValStart;
    //public TMP_Text tempValEnd;
    //public TMP_Text timeVal;
    //public TMP_Text presVal;

    //public InputField tempValF;
    //public InputField timeValF;
    //public InputField presValF;
    //public Text textResult;

    TMP_Text targetField;




    void Change() {
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

            OnChangeVal?.Invoke(targetField);
            gameObject.SetActive(false);
        });

        this.gameObject.SetActive(false);

        
       
    }
    //public void ClickButtonCalc() {
    //    Sintering model = new Sintering(
    //               t0: 20,//Convert.ToDouble(tempValStart.text),//20
    //               tk: 40,//Convert.ToDouble(tempValEnd.text),//temp
    //               l0: 1 * 0.000001,
    //               p0: 40,
    //               tau1: Convert.ToDouble(timeVal.text),//70*60
    //               d: 0.1 * 0.000000001,
    //               db0: 0.35,
    //               ds0: 0.4,
    //               eb: 171.5 * 1000,
    //               es: 245 * 1000,
    //               s: 3.5,
    //               eta0: 170 * 1000000,
    //               pg: 40,//Convert.ToDouble(presVal.text),//press * 1000000
    //               m: 0.1,
    //               ro0: 14600,
    //               tau2: 60 * 60);

    //    var result = model.Calculate(true);
    //    string txt = "Конечный диаметр зерна,мкм = " + result.LL + '\n' +
    //     "Конечная пористость,% = " + result.PP + '\n' +
    //     "Конечная плотность,кг/м^3 = " + result.Ro + "   " + tempValStart.text + "   " + tempValEnd.text + "   " + timeVal.text + "   " + presVal.text;



    //   // printTxt(txt, DateTime.Now);

    //    textResult.text = "Конечный диаметр зерна,мкм = " + result.LL + '\n' +
    //     "Конечная пористость,% = " + result.PP + '\n' +
    //     "Конечная плотность,кг/м^3 = " + result.Ro + "   " + tempValStart.text + "   " + tempValEnd.text + "   " + timeVal.text + "   " + presVal.text;



    //    // var context = new Context("mainV1.db");

    //}
    public void Open(TMP_Text t) {
        targetField = t;
        valText.text = t.text;
        gameObject.SetActive(true);
             
    }
}

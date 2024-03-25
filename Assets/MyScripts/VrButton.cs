using Mathematics;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
                   tau2: 60 * 60);


        var result = model.Calculate(true);
        textResult.text = "Конечный диаметр зерна,мкм = " + result.LL + '\n' +
         "Конечная пористость,% = " + result.PP + '\n' +
         "Конечная плотность,кг/м^3 = " + result.Ro+ "   "+tempValStart.text+ "   " + tempValEnd.text+ "   " + timeVal.text+ "   " + presVal.text;



        // var context = new Context("mainV1.db");

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

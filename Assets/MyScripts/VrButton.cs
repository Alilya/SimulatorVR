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
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.Rendering.DebugUI;
using System.Collections.Generic;
using Entity.Models;
using UnityEngine.UIElements;

public class VrButton : MonoBehaviour {
    public bool isChangeColor = false;
    private Color _color_start;
    private UnityEngine.UI.Image _button;
    [Serializable] public class ButtonEvent : UnityEvent { }

    public ButtonEvent down;
    public ButtonEvent press;
    public ButtonEvent up;
       
    public void Start() {
        
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
            if (gameObject.active)
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

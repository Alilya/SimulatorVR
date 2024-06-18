using Mathematics;
using System;
using UnityEngine;


public class RotateButtons : MonoBehaviour
{
    
    [SerializeField] public UnityEngine.UI.Text textTemp; 
    void OnMouseDrag() {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseDirection = mousePosition - objectPosition;
        double angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis((float)-angle, Vector3.forward);
        textTemp.text = Math.Round(Math.Abs(angle*10),0).ToString();
       // result(angle);
    }

}

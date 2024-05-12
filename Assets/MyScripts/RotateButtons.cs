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
        result(angle);
    }
    public void result(double angle) {
        Sintering model = new Sintering(
               t0: 20,
               tk: 1350,
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
               pg: 6 * 1000000,
               m: 0.1,
               ro0: 14600,
               tau2: 30 * 60);

        var result = model.Calculate(true);

        Debug.Log(result.Ett + " Ett");
        Debug.Log(result.LL + " LL");
        Debug.Log(result.PP + " PP");
        Debug.Log(result.PPP + " PPP");
        Debug.Log(result.Ro + " Ro");

    }

}

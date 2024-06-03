using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.MRTK_Keyboard_main.MRTK.SDK.Experimental.NonNativeKeyboard.Scripts {
    public class Enter : MonoBehaviour {

        public Text result;
        TMP_Text targetField;
        public void enter(TMP_Text text) {
            targetField = text;

            result.text = text.text;
        }
    }
}

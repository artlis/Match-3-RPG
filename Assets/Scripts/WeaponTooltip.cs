using UnityEngine;
using System.Collections;

public class WeaponTooltip : MonoBehaviour {



    public string text = "YOUR TEXT HERE";

    private string currentToolTipText = "";
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;
 
    void Start()
    {
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.white;
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.wordWrap = true;
        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;
        guiStyleBack.alignment = TextAnchor.UpperCenter;
        guiStyleBack.wordWrap = true;
    }

    void OnMouseEnter()
    {

        currentToolTipText = text;
    }

    void OnMouseExit()
    {
        currentToolTipText = "";
    }

    void OnGUI()
    {
        if (currentToolTipText != "")
        {
            float x = Event.current.mousePosition.x;
            float y = Event.current.mousePosition.y;
            GUI.Label(new Rect(x - 149, y + 40, 300, 60), currentToolTipText, guiStyleBack);
            GUI.Label(new Rect(x - 150, y + 40, 300, 60), currentToolTipText, guiStyleFore);
        }
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class AssignedWeapon : MonoBehaviour {

    private Weapon weapon;
    private Player owner;
    private Player enemy;
    public bool active = false;
    private GUIContent weaponContent;
    private GUIStyle weaponStyle;
    public Texture2D image;

    public void Start()
    {
        //more complicated stuff with player stats here
        weaponContent = new GUIContent();
        //weaponContent.image = image;
        weaponStyle = new GUIStyle();
        weaponStyle.wordWrap = true;
    }

    public void Update()
    {
        updateDesc();
    }


    public void updateDesc()
    {

        weaponContent.text = weapon.description;
        weaponContent.text = weaponContent.text.Replace("[str]", weapon.strAbility(owner.str).ToString());
        weaponContent.text = weaponContent.text.Replace("[int]", weapon.inteAbility(owner.inte).ToString());
        weaponContent.text = weaponContent.text.Replace("[dex]", weapon.dexAbility(owner.dex).ToString());
    }

    public void SetVars(Weapon weapon_, Player owner_, Player enemy_)
    {
        weapon = weapon_;
        owner = owner_;
        enemy = enemy_;
    }

    public string getDescription()
    {
        return weapon.description;
    }

    public void displaySubMenu()
    {
        //if (GlobalVars.Instance.currentPlayer == owner)
        //{
        active = true;

        GlobalVars.Instance.weaponButtonsEnabled = false;
        //}
    }

    public void OnGUI()
    {
        if (active)
        {
            float x = gameObject.transform.position.x - 80;
            GUI.Box(new Rect(x, 227, 160, 230), weaponContent, weaponStyle);
            if (GUI.Button(new Rect(x, 197, 160, 30), "Back"))
            {
                active = false;
                GlobalVars.Instance.weaponButtonsEnabled = true; //this is activating before tthe mouse is done clicking and clicks the damn button again. maybe just move the button up?
            }
            if (GlobalVars.Instance.currentPlayer == owner)
            {
                if (GUI.Button(new Rect(x, 457, 160, 30), "Use"))
                {
                    active = false;
                    weapon.useAbility(owner, enemy);
                    GlobalVars.Instance.PlayerSwitch();
                }
            }

            /*if (GUI.Button(new Rect(170, 75, 380, 380), new GUIContent()))
            {
                active = false;
                GlobalVars.Instance.enableEquips();
            }*/
        }
    }
}

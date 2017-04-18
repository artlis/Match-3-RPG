using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

    public Dropdown p1MainHandDrop;
    public Dropdown p2MainHandDrop;

    public Dropdown p1SideHandDrop;
    public Dropdown p2SideHandDrop;

    public Dropdown p1LeftBeltDrop;
    public Dropdown p2LeftBeltDrop;

    public Dropdown p1RightBeltDrop;
    public Dropdown p2RightBeltDrop;

    private Weapon[] p1Equips = new Weapon[4];
    private Weapon[] p2Equips = new Weapon[4];

    public Weapon p1MainHand;
    public Weapon p1SideHand;
    public Weapon p1LeftBelt;
    public Weapon p1RightBelt;

    public Weapon p2MainHand;
    public Weapon p2SideHand;
    public Weapon p2LeftBelt;
    public Weapon p2RightBelt;

    public List<Weapon> weapons = new List<Weapon>();

    private int heavyWepThresh;

    // Use this for initialization
    void Start () {
        List < Dropdown.OptionData > tempOptions = new List<Dropdown.OptionData>();
        tempOptions.Add(new Dropdown.OptionData(""));
        weapons.Add(null);
        int counter = 1;

        //Light weaps


        //belt dropdown
        p1LeftBeltDrop.ClearOptions();
        p1RightBeltDrop.ClearOptions();
        p2LeftBeltDrop.ClearOptions();
        p2RightBeltDrop.ClearOptions();

        while (counter < weapons.Count)
        { 
            tempOptions.Add(new Dropdown.OptionData(weapons[counter].name));
            counter++;
        }
        p1LeftBeltDrop.options = new List<Dropdown.OptionData>(tempOptions);
        p1RightBeltDrop.options = new List<Dropdown.OptionData>(tempOptions);
        p2LeftBeltDrop.options = new List<Dropdown.OptionData>(tempOptions);
        p2RightBeltDrop.options = new List<Dropdown.OptionData>(tempOptions);

        //medium weaps
        weapons.Add(new Sword());
        //side weap dropdown
        p1SideHandDrop.ClearOptions();
        p2SideHandDrop.ClearOptions();

        while (counter < weapons.Count)
        {
            tempOptions.Add(new Dropdown.OptionData(weapons[counter].name));
            counter++;
        }

        p1SideHandDrop.options = new List<Dropdown.OptionData>(tempOptions);
        p2SideHandDrop.options = new List<Dropdown.OptionData>(tempOptions);
        //heavy weaps
        heavyWepThresh = counter-1; // this tells me when two handed weapons start for use in the onweaponselect

        weapons.Add(new Bow());

        //main dropdown

        p1MainHandDrop.ClearOptions();
        p2MainHandDrop.ClearOptions();

        while (counter < weapons.Count)
        {
            tempOptions.Add(new Dropdown.OptionData(weapons[counter].name));
            counter++;
        }

        p1MainHandDrop.options = new List<Dropdown.OptionData>(tempOptions);
        p2MainHandDrop.options = new List<Dropdown.OptionData>(tempOptions);
        //set starting text to first option
        /*p1MainHandDrop.GetComponentInChildren<Text>().text = p1MainHandDrop.options[0].text;
        p2MainHandDrop.GetComponentInChildren<Text>().text = p2MainHandDrop.options[0].text;
        p1SideHandDrop.GetComponentInChildren<Text>().text = p1MainHandDrop.options[0].text;
        p2SideHandDrop.GetComponentInChildren<Text>().text = p2MainHandDrop.options[0].text;
        p1LeftBeltDrop.GetComponentInChildren<Text>().text = p1MainHandDrop.options[0].text;
        p2LeftBeltDrop.GetComponentInChildren<Text>().text = p2MainHandDrop.options[0].text;
        p1RightBeltDrop.GetComponentInChildren<Text>().text = p1MainHandDrop.options[0].text;
        p2RightBeltDrop.GetComponentInChildren<Text>().text = p2MainHandDrop.options[0].text;*/
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public void onWeaponSelect()
    {
        if (p1MainHandDrop.value > heavyWepThresh)
        {
            p1SideHandDrop.value = 0;
            p1SideHandDrop.GetComponentInChildren<Text>().text = "";
            p1SideHandDrop.interactable = false;
        }
        else
        {
            p1SideHandDrop.interactable = true;
        }
        if (p2MainHandDrop.value > heavyWepThresh)
        {
            p2SideHandDrop.value = 0;
            p2SideHandDrop.GetComponentInChildren<Text>().text = "";
            p2SideHandDrop.interactable = false;
        }
        else
        {
            p2SideHandDrop.interactable = true;
        }
    }

    public void switchToBattle()
    {
        if (weapons[p1MainHandDrop.value] != null) { GlobalVars.Instance.player1Data.equipment.Add(weapons[p1MainHandDrop.value]); }
        if (weapons[p1SideHandDrop.value] != null) { GlobalVars.Instance.player1Data.equipment.Add(weapons[p1SideHandDrop.value]); }
        if (weapons[p1LeftBeltDrop.value] != null) { GlobalVars.Instance.player1Data.equipment.Add(weapons[p1LeftBeltDrop.value]); }
        if (weapons[p1RightBeltDrop.value] != null) { GlobalVars.Instance.player1Data.equipment.Add(weapons[p1RightBeltDrop.value]); }

        if (weapons[p2MainHandDrop.value] != null) { GlobalVars.Instance.player2Data.equipment.Add(weapons[p2MainHandDrop.value]); }
        if (weapons[p2SideHandDrop.value] != null) { GlobalVars.Instance.player2Data.equipment.Add(weapons[p2SideHandDrop.value]); }
        if (weapons[p2LeftBeltDrop.value] != null) { GlobalVars.Instance.player2Data.equipment.Add(weapons[p2LeftBeltDrop.value]); }
        if (weapons[p2RightBeltDrop.value] != null) { GlobalVars.Instance.player2Data.equipment.Add(weapons[p2RightBeltDrop.value]); }
        
        SceneManager.LoadScene("Battle");
    }
}

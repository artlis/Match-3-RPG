using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public PlayerData player1Data;
    public PlayerData player2Data;
    public Text player1Turn;
    public Text player2Turn;
    public GameObject weaponButton;
    public List<GameObject> Player1Equips;
    public List<GameObject> Player2Equips;
    private bool enableListener;


    void Start()
    {
        player1Data = GlobalVars.Instance.player1Data;
        player2Data = GlobalVars.Instance.player2Data;

        print(player1Data.equipment.Count);
        createWeaponButtons();
    }
    
    void Update()
    {
        if (GlobalVars.Instance.weaponButtonsEnabled != enableListener)
        {
            enableListener = GlobalVars.Instance.weaponButtonsEnabled;
            if (enableListener)
            {
                enableEquips();
            }
            else
            {
                disableEquips();
            }
        }
    }

    void OnGUI()
    {
        if (GlobalVars.Instance.currentPlayer != null)
        {
            if (GlobalVars.Instance.currentPlayer.playerNum == 1)
            {
                player1Turn.text = "Player 1's Turn";
                player2Turn.text = "";
            }
            else if (GlobalVars.Instance.currentPlayer.playerNum == 2)
            {
                player1Turn.text = "";
                player2Turn.text = "Player 2's Turn";
            }
        }
    }


    private void createWeaponButtons()
    {
        for (int i = 0; i < player1Data.equipment.Count; ++i)
        {
            
            if (player1Data.equipment[i] != null)
            {
                //instantiate button with player useAbility(i);
                GameObject newButton = Instantiate(weaponButton) as GameObject;
                newButton.GetComponent<AssignedWeapon>().SetVars(player1Data.equipment[i], GlobalVars.Instance.player1, GlobalVars.Instance.player2); // give information to attached script
                newButton.transform.SetParent(this.transform, false);
                newButton.GetComponent<RectTransform>().position = new Vector3(-270 + 360.5f, 10 - (70 * i) + 267); // added those variables because idk how to do relative to canvas
                newButton.GetComponentInChildren<Text>().text = player1Data.equipment[i].name;
                Player1Equips.Add(newButton);
            }

        }
        for (int i = 0; i < player2Data.equipment.Count; ++i)
        {
            if (player2Data.equipment[i] != null)
            {
                //instantiate button with player useAbility(i);
                GameObject newButton = Instantiate(weaponButton) as GameObject;
                newButton.GetComponent<AssignedWeapon>().SetVars(player2Data.equipment[i], GlobalVars.Instance.player2, GlobalVars.Instance.player1);
                newButton.transform.SetParent(this.transform, false);
                newButton.GetComponent<RectTransform>().position = new Vector3(270 + 360.5f, 10 - (70 * i) + 267);// added those variables because idk how to do relative to canvas
                newButton.GetComponentInChildren<Text>().text = player2Data.equipment[i].name;
                Player2Equips.Add(newButton);
            }
        }
    }

    public void setActiveEquips(Player currentPlayer)
    {
        foreach (GameObject n in Player1Equips)
        {
            if (currentPlayer.playerNum == 1)
                n.GetComponent<Button>().interactable = true;
            else
                n.GetComponent<Button>().interactable = false;
        }
        foreach (GameObject n in Player2Equips)
        {
            if (currentPlayer.playerNum == 2)
                n.GetComponent<Button>().interactable = true;
            else
                n.GetComponent<Button>().interactable = false;
        }
    }

    public void disableEquips()
    {
        foreach (GameObject n in Player1Equips)
        {
            n.GetComponent<Button>().interactable = false;
        }
        foreach (GameObject n in Player2Equips)
        {
            n.GetComponent<Button>().interactable = false;
        }
    }

    public void enableEquips()
    {
        //if (GlobalVars.Instance.currentPlayer == Player1)
        //{
            //print("player 1");
            foreach (GameObject n in Player1Equips)
            {
                n.GetComponent<Button>().interactable = true;
            }
        //}
        //else
        //{
            //print("player 2");
            foreach (GameObject n in Player2Equips)
            {
                n.GetComponent<Button>().interactable = true;
            }
        //}
    }

    public void playFieldClicked()
    {
        bool wasActive = false;
        foreach (GameObject n in Player1Equips)
        {
            if (n.GetComponent<AssignedWeapon>().active == true)
            {
                wasActive = true;
                n.GetComponent<AssignedWeapon>().active = false;
            }
            
        }
        foreach (GameObject n in Player2Equips)
        {
            if (n.GetComponent<AssignedWeapon>().active == true)
            {
                wasActive = true;
                n.GetComponent<AssignedWeapon>().active = false;
            }
        }
        if (wasActive)
            enableEquips();

    }
}
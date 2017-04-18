using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GlobalVars : MonoBehaviour {

    public Player currentPlayer;
    public Player player1;
    public Player player2;
    
    public PlayerData player1Data = new PlayerData();
    public PlayerData player2Data = new PlayerData();
    

    public bool weaponButtonsEnabled = false;

    // Static singleton property
    public static GlobalVars Instance { get; private set; }

    void Awake()
    {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        // Here we save our singleton instance
        Instance = this;

        // Furthermore we make sure that we don't destroy between scenes (this is optional)
        DontDestroyOnLoad(gameObject);
    }

    public void PlayerSwitch()
    {
        if (currentPlayer == null || currentPlayer == player2)
            currentPlayer = player1;
        else
            currentPlayer = player2;
        
        //MainUI.setActiveEquips(currentPlayer);//enable or disable equip buttons dependinng oon turn
        weaponButtonsEnabled = true;
        
    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

}

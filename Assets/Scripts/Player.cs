using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public int playerNum;
    public PlayerData data;

    private float health;

    private float defense;

    public int str;
    public int inte;
    public int dex;
    public int vit;
    public int luck;

    private int healthMax;
    private int defenseMax;
    private int strMax;
    private int inteMax;
    private int dexMax;

    //mods are based on class and such. multiplies gem gains.
    private float defenseMod;
    private float inteMod;
    private float dexMod;
    private float strMod;

    private int defenseType; //0-2 0 = shield, 1 = parry, 2 = dodge
    //depending on what you have equipped some things are multiplied. (single handed weapons with nothing in side hand gets bonusses while 2 main hand weapons 
    // will reduce effectiveness.
    public float wepLoadMod; 


   // public List<Weapon> equipment = new List<Weapon>();
    //private Gear gear;

    private bool human = true; // not quite sure hwo this works yet but for deciding on ai or click

    public Player opponent;

    //will have to assign at runtime 
    public Slider healthSlider;
    public Slider defenseSlider;
   

    public Text healthText;
    public Text defenseText;
    public Text strText;
    public Text inteText;
    public Text dexText;
    public Text luckText;

    

    // Use this for initialization
    void Awake () {
        //this is where your gear will determine your mod gains and bar caps but default values for now
        defenseMod = 1f;
        strMod = 1f;
        inteMod = 1f;
        dexMod = 1f;

        healthMax = 30;
        defenseMax = 10;
        strMax = 10;
        inteMax = 10;
        dexMax = 10;

        health = healthMax;

        healthSlider.maxValue = healthMax;
        defenseSlider.maxValue = defenseMax;

        wepLoadMod = 1f;

    
        if (playerNum == 1)
        {
            data = GlobalVars.Instance.player1Data;
            GlobalVars.Instance.player1 = this;
        }
        else if (playerNum == 2)
        {
            data = GlobalVars.Instance.player2Data;
            GlobalVars.Instance.player2 = this;
        }
       

    }

    void Start()
    {
        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {
        updateVars();
        updateSliders();
        UpdateText();
    }

    public void updateSliders()
    {
        healthSlider.value = Mathf.MoveTowards(healthSlider.value, health, 1f);
        defenseSlider.value = Mathf.MoveTowards(defenseSlider.value, defense, 1f);
    }

    public void UpdateText()
    {//potential idea to make the numbers same as sliders so theres a kind of slow scaling effect
        healthText.text = "Health: " + health + "/" + healthMax;
        defenseText.text = "Defense: " + defense + "/" + defenseMax;
        strText.text = "STR: \n" + str + "/" + strMax;
        inteText.text = "INT: \n" + inte + "/" + inteMax;
        dexText.text = "DEX: \n" + dex + "/" + dexMax;
        luckText.text = "Luck: " + luck + "%";
    }

    public void updateVars()
    {
        // also this is longwinded as hell. consider moving to arrays ie if stat[i] > statmax[i] so everything will work in like 5 lines.
        if (inte > inteMax)
        {
            inte = inteMax;
        }
        else if (inte < 0)
            inte = 0;
        if (dex > dexMax)
        {
            dex = dexMax;
        }
        else if (dex < 0)
            dex = 0;
        if (defense > defenseMax)
        {
            defense = defenseMax;
        }
        else if (defense < 0)
            defense = 0;
        if (str > strMax)
        {
            str = strMax;
        }
        else if (str < 0)
            str = 0;

        if (luck > 100)
        {
            luck = 100;
        }
        else if (luck < 0)
            luck = 0;
    }

    public void useAbility(int i)
    {
        if (GlobalVars.Instance.currentPlayer == this)
        {
            if (str != 0 || inte != 0 || dex != 0)
            {
                data.equipment[i].useAbility(this, opponent);
                
                GlobalVars.Instance.PlayerSwitch();
            }
        }
    }

    public void damage(float damage)
    {
        float tempdamage = damage;
        damage -= defense;
        defense -= tempdamage;
        if (defense < 0)
            defense = 0;
        if (damage > 0)
            health -= damage;
        if (health < 0)
            health = 0;
    }

    public void damageDefense(float damage)
    {
        defense -= damage;
    }

    public void damageHealth(float damage)
    {
        health -= damage;
        if (health < 0)
            health = 0;
    }

    //this will be called when equipping items in order to make sure item choices are valid
    //THIS IS UP FOR REWORK.
    //light weapons are a 2, medium weapons are a 3, heavy weapons(twohand) are a 5
    // if your total is 7 or higher, unequip side weapon (youre equipping a 2 hander)
    // if your total is higher than 6, all weapon effects are reduced. if your total = 3, you get a bonus. (main hand + empty off hand)

    public void checkEquipLoad()
    {
        int loadCount = 0;
        if (data.equipment[0] != null)
            loadCount += data.equipment[0].load;
        if (data.equipment[1] != null)
            loadCount += data.equipment[0].load;

        if (loadCount > 6) //check for two handers.
        {
            if (data.equipment[0].load == 5) //get rid off offhand
                loadCount -= data.equipment[1].load;
            else
            {
                loadCount -= data.equipment[0].load; // if two hand is in second slot, move to first
                data.equipment[0] = data.equipment[1];
            }

            data.equipment[1] = null;
        }

        if (loadCount == 3) //medium alone
            wepLoadMod = 1.5f;
        if (loadCount == 6) // two mediums.
            wepLoadMod = 0.75f;

        //assign defense type depending on load

        //still figuring out wha t i want to do with this shit

       /* if (equipment[1] == null)
        {
            //assign dodge
        }
        else if (equipment[1].load == 1)
        {
            //assign shield
        }
        else
        {
            //assign parry
        }*/
    }

    public void gainResources(int[] resources)
    {
        if (testLuck())
        {
            for(int i = 0; i < resources.Length; ++i)
            {
                resources[i] *= 2;
            }
        }
        str += resources[0];
        inte += resources[1];
        dex += resources[2];
        defense += resources[3];
        luck += resources[4];

        UpdateText();
    }

    public void gainDefense(int amount)
    {
        defense += amount;
        if (defense > defenseMax) { defense = defenseMax; }
    }

    public bool testLuck()
    {
        if (Random.Range(0, 100) < luck)
        {
            luck = 0;
            //plug in visual cue code here.
            return true;
        }
        return false;
    }
}

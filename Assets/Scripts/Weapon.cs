using UnityEngine;
using System.Collections;

/*notes on weaponnsssssdds
all hand weapons use all stats to varying effectiveness
thats a lot of mechanics to make up though, hm.

*/
public class Weapon {

    

    public int attack; //the attack value that abilities will scale off of
    public int cooldown; // turns before you can use again
    public int load;// 5 = high, 3 = medium, 2 = low.

    public string name = "weapon";
    public string description = "Attack with weapon.";
    


    public virtual void useAbility(Player thisPlayer, Player opposingPlayer)
    {
        //checklist for making new abilities
        //multiply stats and attack by wepLoadMod.
        thisPlayer.str = (int)(thisPlayer.str * thisPlayer.wepLoadMod);
        thisPlayer.inte = (int)(thisPlayer.inte * thisPlayer.wepLoadMod);
        thisPlayer.dex = (int)(thisPlayer.dex * thisPlayer.wepLoadMod);

        int damage = (int)(attack * thisPlayer.wepLoadMod);

        //ability effects
        //str

        //int

        //dex


        //apply damages
        opposingPlayer.damage(damage);

        //remove resources at the end of the mmethod.
        thisPlayer.str = 0;
        thisPlayer.inte = 0;
        thisPlayer.dex = 0;
    }

    public virtual int strAbility(int str)
    {
        return str;
    }

    public virtual int inteAbility(int inte)
    {
        return inte;
    }

    public virtual int dexAbility(int dex)
    {
        return dex;
    }

}

public class Sword : Weapon
{
    //overriding variables
    public Sword()
    {
        attack = 3;
        cooldown = 2;
        load = 3;
        name = "Sword";
        description = "Slash with your sword. Does Str + Weapon Attack ([str]) damage. It will crit based on Int ([int]%). Reduces enemy strength by Dex/2 ([dex]).";
    }

    public override void useAbility(Player thisPlayer, Player opposingPlayer)
    {
        //damage with str, crit with int, weaken with dex
        //apply wep mod
        thisPlayer.str = (int)(thisPlayer.str * thisPlayer.wepLoadMod);
        thisPlayer.inte = (int)(thisPlayer.inte * thisPlayer.wepLoadMod);
        thisPlayer.dex = (int)(thisPlayer.dex * thisPlayer.wepLoadMod);

        //int damage = (int)(attack * thisPlayer.wepLoadMod);

        //str
        int damage = strAbility(thisPlayer.str);

        //int
        if (Random.Range(0,100) < inteAbility(thisPlayer.inte))
        {
            damage = (int) (damage * 1.5);
        }

        //dex
        opposingPlayer.str -= dexAbility(thisPlayer.dex);

        //apply damages
        opposingPlayer.damage(damage);

        //remove resources
        thisPlayer.str = 0;
        thisPlayer.inte = 0;
        thisPlayer.dex = 0;
    }

    public override int strAbility(int str)
    {
        return str + attack;
    }
    public override int inteAbility(int inte)
    {
        return inte;
    }
    public override int dexAbility(int dex)
    {
        return dex/2;
    }
}

public class Bow : Weapon
{
    //overriding variables
    public Bow()
    {
        attack = 3;
        cooldown = 2;
        load = 5;
        name = "Bow";
        description = "Shoot with your bow. Deals Str + Weapon Attack ([str]) damage. Deals Dex ([dex]) + Int ([int]) damage to defense.";
    }

    public override void useAbility(Player thisPlayer, Player opposingPlayer)
    {
        //damage scales on str, dex and int destroy defense
        //apply wep mod
        thisPlayer.str = (int)(thisPlayer.str * thisPlayer.wepLoadMod);
        thisPlayer.inte = (int)(thisPlayer.inte * thisPlayer.wepLoadMod);
        thisPlayer.dex = (int)(thisPlayer.dex * thisPlayer.wepLoadMod);

        int damage = strAbility(thisPlayer.str);
        //apply effects
        //none

        //apply damages
        opposingPlayer.damageDefense(dexAbility(thisPlayer.dex )+ inteAbility(thisPlayer.inte));
        opposingPlayer.damage(damage);

        // remove resources
        thisPlayer.str = 0;
        thisPlayer.inte = 0;
        thisPlayer.dex = 0;
    }

    public override int strAbility(int str)
    {
        return str + attack;
    }
    public override int inteAbility(int inte)
    {
        return inte;
    }
    public override int dexAbility(int dex)
    {
        return dex;
    }
}

public class Dagger : Weapon
{
    //overriding variables
    public Dagger()
    {
        attack = 3;
        cooldown = 2;
        load = 5;
        name = "Dagger";
        description = "Stab with your dagger. Deals Dex + Weapon Attack ([dex]) damage. ";
    }

    public override void useAbility(Player thisPlayer, Player opposingPlayer)
    {
        //damage scales on str, dex and int destroy defense
        //apply wep mod
        thisPlayer.str = (int)(thisPlayer.str * thisPlayer.wepLoadMod);
        thisPlayer.inte = (int)(thisPlayer.inte * thisPlayer.wepLoadMod);
        thisPlayer.dex = (int)(thisPlayer.dex * thisPlayer.wepLoadMod);

        int damage = dexAbility(thisPlayer.str);
        //apply effects
        //none

        //apply damages
        opposingPlayer.damage(damage);

        // remove resources
        //thisPlayer.str = 0;
        //thisPlayer.inte = 0;
        thisPlayer.dex = 0;
    }

    public override int strAbility(int str)
    {
        return str;
    }
    public override int inteAbility(int inte)
    {
        return inte;
    }
    public override int dexAbility(int dex)
    {
        return dex + attack;
    }
}

/*
 * *possible mechanics
 * stat reduction
 * stat gain
 * damage
 * defense damage
 * health damage
 * cooldown increease
 * cooldown decrease
 * destroy gems
 * -by type
 * -by row
 * -by column
 * -by random
 * extra turn
 * status effect
 * -poison
 * -burning
 * -silence(?)
 * -stun
 * -
 * health gain 
 * 
 * what elseeeeeeeeeeeeeeeeeeeeeeee
 * */
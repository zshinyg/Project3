using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class invincibilityItem : IItem 
{

    private int durationLength;

    public void Ability(Player player){
        changeInviciblity(player);
    }

    public int Duration(){
        durationLength = Random.Range(1,3);
        durationLength = durationLength * 10;
        return durationLength;
    }

    private void changeInviciblity(Player player){
        player.setInvincibility(true);
    }
}

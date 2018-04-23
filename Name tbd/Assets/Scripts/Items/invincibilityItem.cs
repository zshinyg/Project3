using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class invincibilityItem : MonoBehaviour, IItem 
{

    private int durationLength;

    public void Ability(IPlayer player){
        changeInviciblity(player);
        Duration(player);
    }

    public void Duration(IPlayer player){
        durationLength = Random.Range(1,3);
        durationLength = durationLength * 10;
        player.setIDuration(durationLength);
    }

    private void changeInviciblity(IPlayer player){
        player.setInvincibility(true);
    }
}

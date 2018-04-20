using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class invincibilityItem : MonoBehaviour, IItem 
{
    private int duration; 
    private int durationLength;

    public void Ability(IPlayer player){
        changeInviciblity(player);
    }

    public int Duration(){
        durationLength = Random.Range(1,3);
        durationLength = durationLength * 10;
        return durationLength;
    }

    private void changeInviciblity(IPlayer player){
        duration = Duration();
        player.setInvincibility(true);
        Debug.Log("Duration: " + duration);
    }
}

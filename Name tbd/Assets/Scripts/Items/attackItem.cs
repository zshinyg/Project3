using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class attackItem : MonoBehaviour, IItem 
{

    private float changeToAttack;
    private int durationLength;

    public void Ability(IPlayer player){
        changeAttack(player);
        Duration(player);
    }

    public void Duration(IPlayer player){
        durationLength = Random.Range(1,3);
        durationLength = durationLength * 10;
        player.setADuration(durationLength);
    }

    public void changeAttack(IPlayer player){
        changeToAttack = Random.Range(3,10);
        changeToAttack = changeToAttack/10;
		player.setAttack((int)(player.getAttack() * (1 + changeToAttack)));
    }
}

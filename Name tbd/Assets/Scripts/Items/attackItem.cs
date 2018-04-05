using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class attackItem : IItem 
{

    private float changeToAttack;
    private int durationLength;

    public void Ability(Player player){
        changeAttack(player);
    }

    public int Duration(){
        durationLength = Random.Range(1,3);
        durationLength = durationLength * 10;
        return durationLength;
    }

    public void changeAttack(Player player){
        changeToAttack = Random.Rand(3,10);
        changeToAttack = changeToAttack/10;
        player.setAttack(player.getAttack() * (1 + changeAttack));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class attackItem : MonoBehaviour, IItem 
{

    private float changeToAttack;
    private int durationLength;

    public void Ability(IPlayer player){
        changeAttack(player);
    }

    public int Duration(){
        durationLength = Random.Range(1,3);
        durationLength = durationLength * 10;
        return durationLength;
    }

    public void changeAttack(IPlayer player){
        changeToAttack = Random.Range(3,10);
        changeToAttack = changeToAttack/10;
		player.setAttack(player.getAttack() * (int)(1 + changeToAttack));
    }
}

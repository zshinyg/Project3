using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class healthItem : MonoBehaviour, IItem
{

    private int changeToHealth;
    public void Ability(IPlayer player){
          
          changeHealth(player);
    }

    public int Duration(){
        return 0;
    }

    private void changeHealth(IPlayer player){

        changeToHealth = Random.Range(-5,5);
        while (changeToHealth == 0){
            changeToHealth = Random.Range(-5,5);
        }

        changeToHealth = changeToHealth*10;
        //Debug.Log(changeToHealth);
		player.setHealth(player.getHealth() + changeToHealth);
    }
}
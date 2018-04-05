using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public calss healthItem : IItem
{

    private int changeToHealth;
    public void Ability(Player player){
          
          changeHealth(player);
    }

    public int Duration(){
        return 0;
    }

    private void changeHealth(Player player){

        changeToHealth = Random.Range(-5,5);
        while (changeToHealth == 0){
            changeToHealth = Random.Range(-5,5);
        }

        changeToHealth = changeToHealth*10;
        player.setHealth(player.getHealth() + changeToHealth);
    }
}
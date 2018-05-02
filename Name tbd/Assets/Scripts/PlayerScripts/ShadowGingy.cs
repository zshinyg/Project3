using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowGingy : IPlayer
{
    public int Health = 100;

    public int GetHealth()
    {
        return Health;
    }

    public void SetHealth(int value)
    {
        Health = value;
    }
}

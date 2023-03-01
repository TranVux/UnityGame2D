using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConstantManager
{
    private static PlayerConstantManager playerConstantManager;

    public int coins = -1;
    public int playAmount = 3;

    private PlayerConstantManager() { }

    public static PlayerConstantManager GetInstance()
    {
        if (playerConstantManager == null)
        {
            playerConstantManager = new PlayerConstantManager();
        }
        return playerConstantManager;
    }

}

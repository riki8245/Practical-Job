using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool c_enableHDR;
    public bool c_enableMSAA;
    public bool c_enableShadows;
    public int currentLevel;

    public GameData(GameManager gameManager) 
    {
        c_enableHDR = gameManager.c_enableHDR;
        c_enableMSAA = gameManager.c_enableMSAA;
        c_enableShadows = gameManager.c_enableShadows;
        currentLevel = gameManager.currentLevel;
    }
}

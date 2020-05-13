using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData : MonoBehaviour
{
    public bool c_enableHDR;
    public bool c_enableMSAA;
    public bool c_enableShadows;
    public int currentLevel;

    public GameData(GameManager gameManager) 
    {
        this.c_enableHDR = gameManager.c_enableHDR;
        this.c_enableMSAA = gameManager.c_enableMSAA;
        this.c_enableShadows = gameManager.c_enableShadows;
        this.currentLevel = gameManager.currentLevel;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("PlayerSettings")]
    public bool c_enableHDR;
    public bool c_enableMSAA;
    public bool c_enableShadows;
    public int  currentLevel;


    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        GameData data = SaveSystem.LoadGame();

        this.c_enableHDR     = data.c_enableHDR;
        this.c_enableMSAA    = data.c_enableMSAA;
        this.c_enableShadows = data.c_enableShadows;
        this.currentLevel    = data.currentLevel;
    }

}

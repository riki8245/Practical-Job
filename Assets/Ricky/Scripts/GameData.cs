[System.Serializable]
public class GameData
{
    public bool c_enableHDR;
    public bool c_enableMSAA;
    public bool c_enableShadows;
    public int currentLevel;
    public int c_Antialising;

    public GameData(GameManager gameManager) 
    {
        c_enableHDR = gameManager.c_enableHDR;
        c_enableMSAA = gameManager.c_enableMSAA;
        c_enableShadows = gameManager.c_enableShadows;
        currentLevel = gameManager.currentLevel;
        c_Antialising = gameManager.c_Antialising;
    }
}

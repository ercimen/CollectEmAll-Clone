using System.Collections.Generic;
using UnityEngine;

public class LevelDataLoader
{
    private const string LevelDataFolder = "Data/LevelData/Level";
    public  LevelData GetLevel(int levelNumber)
    {
        string fileName = LevelDataFolder + levelNumber.ToString() + ".json";
        LevelData jsonFile = JsonDataHelper.LoadJsonData<LevelData>(fileName);
        return jsonFile;
    }
}
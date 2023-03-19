using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private const string LevelPrefKey = "CurrentLevel";
    private int currentLevel;

    private void Awake()
    {
        CheckLevelNumber();
    }

    private void CheckLevelNumber()
    {
        if (!PlayerPrefs.HasKey(LevelPrefKey))
        {
            PlayerPrefs.SetInt(LevelPrefKey, 1);
        }

        currentLevel = PlayerPrefs.GetInt(LevelPrefKey);
    }

    private void Start()
    {
        StartLevel(currentLevel);
    }

    public void StartLevel(int levelNumber)
    {
        currentLevel = levelNumber;

        LevelDataLoader levelDataLoader = new LevelDataLoader();
        
        LevelData currentLevelData = levelDataLoader.GetLevel(currentLevel);

#if UNITY_EDITOR
        Debug.LogWarning("Current Level Number:" + currentLevelData.level_number);
        Debug.LogWarning("Row:" + currentLevelData.row);
#endif

        GridManager.Instance.InitializeGrid(currentLevelData);
    }

    public void LevelCompleted()
    {
        currentLevel++;
        PlayerPrefs.SetInt(LevelPrefKey, currentLevel);
    }
}

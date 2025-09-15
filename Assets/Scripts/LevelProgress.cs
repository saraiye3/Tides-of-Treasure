using UnityEngine;

// this class keeps user level data
public static class LevelProgress
{
    private const string Key = "UnlockedLevels";
    private const string LastLevelKey = "LastLevel"; // חדש: שמירת השלב האחרון

    // defaulte - level 1 locked
    public static int GetUnlockedLevels() => PlayerPrefs.GetInt(Key, 1);

    public static bool IsLevelUnlocked(int stageIndex)
        => stageIndex <= GetUnlockedLevels();

    public static void UnlockUpTo(int stageIndex)
    {
        int cur = GetUnlockedLevels();
        if (stageIndex > cur)
        {
            PlayerPrefs.SetInt(Key, stageIndex);
            PlayerPrefs.Save();
        }
    }

    public static void ResetProgress()
    {
        PlayerPrefs.DeleteKey(Key);
        PlayerPrefs.DeleteKey(LastLevelKey);
    }

    public static void SetLastLevel(int stageIndex)
    {
        PlayerPrefs.SetInt(LastLevelKey, stageIndex);
        PlayerPrefs.Save();
    }

    public static int GetLastLevel()
    {
        return PlayerPrefs.GetInt(LastLevelKey, 1); // ברירת מחדל: Level1
    }
}

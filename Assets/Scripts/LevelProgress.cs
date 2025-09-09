using UnityEngine;
//this cless keep user level data
public static class LevelProgress
{
    private const string Key = "UnlockedLevels";
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

    public static void ResetProgress() => PlayerPrefs.DeleteKey(Key);
}

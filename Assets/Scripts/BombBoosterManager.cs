using UnityEngine;

public static class BombBoosterManager
{
    private const string BOMB_BOOSTER_KEY = "BombBooster_Count";

    // Add bomb booster to inventory
    public static void AddBombBooster()
    {
        int currentCount = GetBombBoosterCount();
        PlayerPrefs.SetInt(BOMB_BOOSTER_KEY, currentCount + 1);
        PlayerPrefs.Save();

        // Notify HUD to update display
        UpdateHUDDisplay();
    }

    // Use bomb booster (decrease inventory by 1)
    public static bool UseBombBooster()
    {
        int currentCount = GetBombBoosterCount();

        if (currentCount > 0)
        {
            PlayerPrefs.SetInt(BOMB_BOOSTER_KEY, currentCount - 1);
            PlayerPrefs.Save();

            UpdateHUDDisplay();

            return true;
        }

        return false;
    }

    // Get current bomb booster count in inventory
    public static int GetBombBoosterCount()
    {
        return PlayerPrefs.GetInt(BOMB_BOOSTER_KEY, 0);
    }

    // Check if bomb boosters are available
    public static bool HasBombBoosters()
    {
        return GetBombBoosterCount() > 0;
    }

    // Reset inventory 
    public static void ResetBombBoosters()
    {
        PlayerPrefs.DeleteKey(BOMB_BOOSTER_KEY);
        PlayerPrefs.Save();
        UpdateHUDDisplay();
    }

    // Update HUD display
    private static void UpdateHUDDisplay()
    {
        HUD hud = Object.FindObjectOfType<HUD>();
        if (hud != null)
        {
            hud.UpdateBombBoosterDisplay(GetBombBoosterCount());
        }
    }

    // Give free booster (for debugging purposes)
    public static void GiveFreeBombBooster()
    {
        AddBombBooster();
        Debug.Log("Free bomb booster given! Total: " + GetBombBoosterCount());
    }
}
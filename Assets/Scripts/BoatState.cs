using UnityEngine;
//save the boat in current position(level)
public static class BoatState
{
    const string X = "BoatX";
    const string Y = "BoatY";
    const string STAGE = "BoatStage"; 

    public static void Save(Vector2 pos, int stageIndex)
    {
        PlayerPrefs.SetFloat(X, pos.x);
        PlayerPrefs.SetFloat(Y, pos.y);
        PlayerPrefs.SetInt(STAGE, stageIndex);
        PlayerPrefs.Save();
    }

    public static bool TryLoad(out Vector2 pos, out int stageIndex)
    {
        stageIndex = PlayerPrefs.GetInt(STAGE, 0);
        pos = new Vector2(PlayerPrefs.GetFloat(X, 0f), PlayerPrefs.GetFloat(Y, 0f));
        return PlayerPrefs.HasKey(STAGE);
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteKey(X);
        PlayerPrefs.DeleteKey(Y);
        PlayerPrefs.DeleteKey(STAGE);
    }
}

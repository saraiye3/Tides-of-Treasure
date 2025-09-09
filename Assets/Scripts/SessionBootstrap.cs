using UnityEngine;
//reset the game after 1 full play
public class SessionBootstrap : MonoBehaviour
{
    private static bool sessionInitialized = false;

    void Awake()
    {
        if (!sessionInitialized)
        {
            LevelProgress.ResetProgress(); 
            BoatState.Reset();             
            sessionInitialized = true;
        }
    }

    void OnApplicationQuit()
    {
        LevelProgress.ResetProgress();
        BoatState.Reset();
        sessionInitialized = false;
    }
}

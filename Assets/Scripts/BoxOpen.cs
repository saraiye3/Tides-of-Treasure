using UnityEngine;

public class BoxOpen : MonoBehaviour
{
    public ParticleSystem coinsPS;   // למקרה של עולם תלת-ממדי
    public MonoBehaviour uiParticle; 

    // נקרא מתוך Event של האנימציה
    public void PlayCoinsFX()
    {
        if (coinsPS != null)
        {
            coinsPS.Clear(true);
            coinsPS.Play(true);
        }


    }
}

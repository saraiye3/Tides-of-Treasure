using UnityEngine;

public class BoxFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinFx;

    public void PlayOpenFx()
    {
        if (coinFx != null)
            coinFx.Play();
    }
}

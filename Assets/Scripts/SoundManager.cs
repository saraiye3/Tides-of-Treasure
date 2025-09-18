using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Clips")]
    public AudioClip backgroundMusic;
    public AudioClip matchSound;
    public AudioClip bigMatchSound;
    public AudioClip specialPieceSound;


    [Header("Bomb")]
    public AudioClip bombSound;

    public void PlayBomb() => sfxSource.PlayOneShot(bombSound);

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    void OnEnable()
    {
        GameEvents.OnMatch += PlayMatch;
        GameEvents.OnBigMatch += PlayBigMatch;
        GameEvents.OnSpecialPiece += PlaySpecialPiece;
        GameEvents.OnBomb += PlayBomb;  
    }

    void OnDisable()
    {
        GameEvents.OnMatch -= PlayMatch;
        GameEvents.OnBigMatch -= PlayBigMatch;
        GameEvents.OnSpecialPiece -= PlaySpecialPiece;
        GameEvents.OnBomb -= PlayBomb;
    }

    public void PlayMatch() => sfxSource.PlayOneShot(matchSound);
    public void PlayBigMatch() => sfxSource.PlayOneShot(bigMatchSound);
    public void PlaySpecialPiece() => sfxSource.PlayOneShot(specialPieceSound);
}
